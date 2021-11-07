using System.Threading.Tasks;
using ThreePoint.Core.Enities.ServiceModel;
using ThreePoint.Core.ViewModel;
using ThreePoint.IRepository;
using ThreePoint.Repository.DbContextService;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ThreePoint.Core.Exceptions;
using ThreePoint.Core.Extensions;
using ThreePoint.Core.Enities;
using System;
using ThreePoint.Core.ServerModels;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using ThreePoint.Core.Filters;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;

namespace ThreePoint.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly ILogger<AdminRepository> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminRepository(IDbContextFactory dbContextFactory, ILogger<AdminRepository> logger, IHttpContextAccessor httpContextAccessor)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> UpdataGeneralInfoAsync(AdminGeneralInfoDto dto)
        {
            int i;
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write))
            {
                var entity = await dbContext.Admins.FirstOrDefaultAsync(x => x.Id == dto.Id);
                if (entity == null) throw new BusinessException("无此管理员", 403);
                if (dto.RealName.IsNotBlank()) entity.RealName = dto.RealName;
                if (dto.Email.IsNotBlank()) entity.Email = dto.Email;
                if (dto.Password.IsNotBlank()) entity.Password = dto.Password;
                dbContext.Admins.Update(entity);
                i = await dbContext.SaveChangesAsync();
            }
            return i <= 0 ? false : true;
        }

        public async Task<AdminEntity> FindAsync(string id)
        {
            AdminEntity admin;
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                admin = await dbContext.Admins.AsNoTracking().Include(x => x.Role).Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);
            }
            return admin;
        }

        public async Task<ResultModel<AdminTableViewModel>> LoginAsync(LoginViewModel dto)
        {
            var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write);
            var loginName = dto.LoginName.ToLower().Trim();
            var entity = await dbContext.Admins.Include(x => x.Department).Include(x => x.Role).FirstOrDefaultAsync(item => item.LoginName == loginName && item.IsDeleted == false);
            if (entity == null) return new ResultModel<AdminTableViewModel>(403, "用户不存在", false);
            if (entity.Password != dto.Password) return new ResultModel<AdminTableViewModel>(403, "用户名密码不正确", false);
            var loginLog = new LoginLogEntity
            {
                UserId = entity.Id,
                Id = Guid.NewGuid().ToString("N"),
                LoginName = dto.LoginName,
                IP = dto.LoginIP,
                Message = "登陆成功",
                CreateDateTime = DateTime.Now
            };
            dbContext.LoginLogs.Add(loginLog);
            await dbContext.SaveChangesAsync();
            AdminTableViewModel adm = new AdminTableViewModel()
            {
                Id = entity.Id,
                LoginName = entity.LoginName,
                RealName = entity.RealName,
                Email = entity.Email,
                CreateDateTime = entity.CreateDateTime.ToString("f"),
                DepartmentName = entity.Department != null ? entity.Department.Name : null,
                RoleId = entity.RoleId,
                RoleName = entity.Role != null ? entity.Role.Name : null,
                Enable = entity.Enabled
            };
            return new ResultModel<AdminTableViewModel>(200, false, "用户登陆成功", adm);
        }

        public async Task<bool> UpdataPassWordAsync(AdminEditPassWordDto dto)
        {
            int i;
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write))
            {
                var entity = await dbContext.Admins.FirstOrDefaultAsync(x => x.Id == dto.Id);
                if (entity == null) throw new BusinessException("无此管理员", 403);
                if (entity.Password != dto.OldPassWord) throw new BusinessException("原密码错误", 403);
                entity.Password = dto.NewPassWord;
                dbContext.Update(entity);
                i = await dbContext.SaveChangesAsync();
            }
            return i <= 0 ? false : true;
        }

        public async Task<AdminGeneralInfoDto> FindGeneralInfoAsync(string id)
        {
            if (id.IsBlank()) throw new BusinessException("id不能为空", 403);
            AdminGeneralInfoDto dto = new AdminGeneralInfoDto();
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                var entity = await dbContext.Admins.FirstOrDefaultAsync(x => x.Id == id);
                if (entity == null) throw new BusinessException("无此管理员", 403);
                dto.Id = entity.Id;
                dto.RealName = entity.RealName;
                dto.Email = entity.Email;
            }
            return dto;
        }

        public async Task<bool> DeleteAsync(IEnumerable<string> ids)
        {
            var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write);
            var strategy = dbContext.Database.CreateExecutionStrategy();
            var ok = true;
            await strategy.Execute(async () =>
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var entities = dbContext.Admins.Where(x => ids.Contains(x.Id));
                        dbContext.RemoveRange(entities);
                        var i = await dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                        if (i <= 0) ok = false;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "删除管理员失败");
                        await transaction.RollbackAsync();
                        ok = false;
                    }
                }
            });
            return ok;
        }

        public async Task<int> GetCountAsync()
        {
            int i;
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                i = await dbContext.Admins.CountAsync();
            }
            return i;
        }

        public async Task<PageResult<AdminEntity>> GetAsync(Expression<Func<AdminEntity, bool>> whereLambda, BaseFilter filter)
        {
            PageResult<AdminEntity> result = new PageResult<AdminEntity>();
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                var entity = dbContext.Admins.AsNoTracking().Include(x => x.Department).Include(x => x.Role).Where(whereLambda);
                result.records = await entity.CountAsync();
                if (filter.Limit < 10) filter.Limit = 10;
                if (filter.Offset < 0) filter.Offset = 0;
                result.rows = await entity.AsNoTracking().Skip(filter.Offset).Take(filter.Limit).ToListAsync();
            }
            return result;
        }

        public async Task<bool> UpdataAsync(AdminDto dto)
        {
            int i = 0;
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write))
            {
                var entity = await dbContext.Admins.FirstOrDefaultAsync(x => x.Id == dto.Id);
                if (entity == null) return false;
                entity.RealName = dto.RealName;
                entity.Email = dto.Email;
                entity.Enabled = dto.Enabled;
                entity.DepartmentId = dto.DepartmentId;
                entity.RoleId = dto.RoleId;
                entity.UpdataUserId = _httpContextAccessor.HttpContext.Session.GetString("Uid");
                entity.UpdataTime = DateTime.Now;
                if (dto.Password.IsNotBlank()) entity.Password = dto.Password.ToMd5();
                dbContext.Update(entity);
                i = await dbContext.SaveChangesAsync();
            }
            return i > 0 ? true : false;
        }

        public async Task<bool> AddAsync(AdminDto dto)
        {
            int i = 0;
            if (dto == null) return false;
            if (dto.Password == null) return false;
            else dto.Password = dto.Password.ToMd5();

            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write))
            {
                AdminEntity entity = new AdminEntity();
                entity.CreateBy(_httpContextAccessor.HttpContext.Session.GetString("Uid"));
                entity.LoginName = dto.LoginName;
                entity.RealName = dto.RealName;
                entity.Email = dto.Email;
                entity.Password = dto.Password;
                entity.DepartmentId = dto.DepartmentId;
                entity.RoleId = dto.RoleId;
                await dbContext.AddAsync(entity);
                i = await dbContext.SaveChangesAsync();
            }
            return i > 0 ? true : false;
        }

        public async Task<bool> CheckPermitAsync(string roleId, string url)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                return await dbContext.RoleMenus.Where(x => x.RoleId == roleId).AnyAsync(x => x.Menu.Url == url);
            }
        }

        public async Task<bool> IsExist(string id)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                return await dbContext.Admins.Where(x => x.IsDeleted == false).AnyAsync(x => x.Id == id);
            }
        }

        public async Task<List<DropDownAdminViewModel>> DropDownAdminSeachAsync(DropDownAdminFilter filter)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                var viewModel = new List<DropDownAdminViewModel>();
                var entity = await dbContext.Admins.Select(x => new { x.Id, x.RealName }).ToListAsync();
                foreach (var item in entity)
                {
                    viewModel.Add(new DropDownAdminViewModel()
                    {
                        Id = item.Id,
                        Name = item.RealName
                    });
                }
                return viewModel;
            }
        }
    }
}