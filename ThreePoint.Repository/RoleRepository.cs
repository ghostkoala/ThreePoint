using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using ThreePoint.Core.Enities;
using ThreePoint.IRepository;
using ThreePoint.Repository.DbContextService;
using Microsoft.EntityFrameworkCore;
using ThreePoint.Core.Filters;
using ThreePoint.Core.Exceptions;
using ThreePoint.Core.Extensions;
using ThreePoint.Core.ServerModels;
using Microsoft.Extensions.Logging;
using ThreePoint.Core.ViewModel;
using ThreePoint.Core.Enities.ServiceModel;
using Microsoft.AspNetCore.Http;

namespace ThreePoint.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly ILogger<RoleRepository> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoleRepository(IDbContextFactory dbContextFactory, ILogger<RoleRepository> logger, IHttpContextAccessor httpContextAccessor)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> AddAsync(RoleEntity entity)
        {
            if (entity == null) throw new BusinessException("角色数据不能为空", 403);
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write))
            {
                entity.CreateUserId = _httpContextAccessor.HttpContext.Session.GetString("Uid");
                await dbContext.Roles.AddAsync(entity);
                await dbContext.SaveChangesAsync();
            };
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(IEnumerable<string> ids)
        {
            if (ids == null) return false;
            var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write);
            var strategy = dbContext.Database.CreateExecutionStrategy();
            var ok = true;
            await strategy.Execute(async () =>
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var entities = dbContext.Roles.Where(x => ids.Contains(x.Id));
                        dbContext.Roles.RemoveRange(entities);
                        var permission = dbContext.RoleMenus.Where(x => ids.Contains(x.RoleId));
                        dbContext.RoleMenus.RemoveRange(permission);
                        var i = await dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                        if (i <= 0) ok = false;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "删除菜单失败");
                        await transaction.RollbackAsync();
                        ok = false;
                    }
                }
            });
            return ok;
        }

        public async Task<List<DropDownRoleViewModel>> DropDownRoleSearchAsync(DropDownRoleFilter filter)
        {
            List<DropDownRoleViewModel> viewModels = new List<DropDownRoleViewModel>();
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                var entity = await dbContext.Roles.Select(x => new { x.Id, x.Name }).ToListAsync();
                foreach (var item in entity)
                {
                    viewModels.Add(new DropDownRoleViewModel()
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }
            }
            return viewModels;
        }

        public async Task<bool> EditPermissionWithRoleAsync(string roleId, IEnumerable<string> ids)
        {
            var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write);
            if (await dbContext.Roles.CountAsync(x => x.Id == roleId) <= 0) throw new BusinessException("无此角色", 403);
            var strategy = dbContext.Database.CreateExecutionStrategy();
            var ok = true;
            await strategy.Execute(async () =>
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var oldPermission = dbContext.RoleMenus.Where(x => x.RoleId == roleId);
                        dbContext.RoleMenus.RemoveRange(oldPermission);
                        List<RoleMenuEntity> permission = new List<RoleMenuEntity>();
                        foreach (var item in ids)
                        {
                            permission.Add(new RoleMenuEntity()
                            {
                                Id = Guid.NewGuid().ToString("N"),
                                RoleId = roleId,
                                MenuId = item
                            });
                        }
                        await dbContext.RoleMenus.AddRangeAsync(permission);
                        var i = await dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                        ok = (i > 0) ? true : false;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "修改限权失败");
                        await transaction.RollbackAsync();
                        ok = false;
                    }
                }
            });
            return ok;
        }

        public async Task<RoleEntity> FindAsync(string id)
        {
            if (id.IsBlank()) return null;
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                return await dbContext.Roles.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<PageResult<RoleEntity>> GetAsync(Expression<Func<RoleEntity, bool>> whereLambda, BaseFilter filter)
        {
            if (filter == null) filter = new BaseFilter();
            PageResult<RoleEntity> result = new PageResult<RoleEntity>();
            //var roles = new List<RoleEntity>();
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                var rolesList = dbContext.Roles.Where(whereLambda);
                result.records = await rolesList.CountAsync();
                if (filter.Limit < 10) filter.Limit = 10;
                if (filter.Offset < 0) filter.Offset = 0;
                result.rows = await rolesList.Skip(filter.Offset).Take(filter.Limit).ToListAsync();
            }
            return result;
        }

        public async Task<int> GetCountAsync()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                return await dbContext.Roles.CountAsync();
            }
        }

        public async Task<List<string>> GetMenuIdWithRole(string roleId)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                var list = await dbContext.RoleMenus.Where(x => x.RoleId == roleId).Select(x => x.MenuId).ToListAsync();
                return list;
            }
        }

        public async Task<bool> UpdataAsync(RoleDto dto)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write))
            {
                var entity = await dbContext.Roles.FirstOrDefaultAsync(x => x.Id == dto.Id);
                if (entity == null) return false;
                entity.Name = dto.Name;
                entity.Description = dto.Description;
                entity.Enabled = dto.Enabled;
                dbContext.Roles.Update(entity);
                var i = await dbContext.SaveChangesAsync();
                if (i <= 0) return false;
            }
            return true;
        }
    }
}