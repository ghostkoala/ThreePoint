using System.Collections.Generic;
using System.Threading.Tasks;
using Logicore.Core.Filters;
using Logicore.Core.ViewModel;
using Logicore.IRepository;
using Logicore.Repository.DbContextService;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Logicore.Core.Enities.ServiceModel;
using System.Linq.Expressions;
using System;
using Logicore.Core.Enities;
using Logicore.Core.ServerModels;
using Logicore.Core.Exceptions;
using Logicore.Core.Extensions;

namespace Logicore.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly ILogger<RoleRepository> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DepartmentRepository(IDbContextFactory dbContextFactory, ILogger<RoleRepository> logger, IHttpContextAccessor httpContextAccessor)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> AddAsync(DepartmentDto dto)
        {
            if (dto == null) throw new ServerException("无效数据", 401);
            var entity = new DepartmentEntity();
            entity.CreateBy(_httpContextAccessor.HttpContext.Session.GetString("Uid"));
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write))
            {
                entity.Name = dto.Name;
                entity.ParentId = dto.ParentId;
                var ParentName = (await dbContext.Departments.FirstOrDefaultAsync(x => x.Id == dto.ParentId)).Name;
                if (ParentName.IsBlank()) throw new ServerException("无效数据，无此父级部门", 401);
                entity.FullName = ParentName + " - " + dto.Name;
                await dbContext.Departments.AddAsync(entity);
                var i = await dbContext.SaveChangesAsync();
                return i > 0 ? true : false;
            }
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
                        var entities = dbContext.Departments.Where(x => ids.Contains(x.Id));
                        //同时删除还有子菜单
                        // foreach (var item in entities)
                        // {
                        //     var childEntity = await dbContext.Menus.Where(x => x.ParentId == item.Id).ToListAsync();
                        //     dbContext.Menus.Remove(item);
                        //     dbContext.Menus.RemoveRange(childEntity);
                        // }
                        dbContext.RemoveRange(entities);
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

        public async Task<List<DropDownDepartmentViewModel>> DropDownDepartmentSeachAsync(DropDownDepartmentFilter filter)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                var viewModel = new List<DropDownDepartmentViewModel>();
                var entity = await dbContext.Departments.Select(x => new { x.Id, x.Name }).ToListAsync();
                foreach (var item in entity)
                {
                    viewModel.Add(new DropDownDepartmentViewModel()
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }
                return viewModel;
            }
        }

        public async Task<bool> EditAsync(DepartmentDto dto)
        {
            if (dto == null) throw new ServerException("无效数据", 401);
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write))
            {
                var entity = await dbContext.Departments.FirstOrDefaultAsync(x => x.Id == dto.Id);
                if (entity == null) throw new ServerException("无此部门", 401);
                entity.Name = dto.Name;
                entity.ParentId = dto.ParentId;
                entity.Enable = dto.Enabled;
                var ParentName = (await dbContext.Departments.FirstOrDefaultAsync(x => x.Id == dto.ParentId)).Name;
                if (ParentName.IsBlank()) throw new ServerException("无效数据，无此父级部门", 401);
                entity.FullName = ParentName + " - " + dto.Name;
                dbContext.Departments.Update(entity);
                var i = await dbContext.SaveChangesAsync();
                return i > 0 ? true : false;
            }
        }

        public async Task<DepartmentEntity> FindAsync(string id)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                return await dbContext.Departments.FirstAsync(x => x.Id == id);
            }
        }

        public async Task<PageResult<DepartmentEntity>> GetAsync(Expression<Func<DepartmentEntity, bool>> whereLambda, BaseFilter filter)
        {
            PageResult<DepartmentEntity> result = new PageResult<DepartmentEntity>();
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                var entity = dbContext.Departments.AsNoTracking().Where(whereLambda);
                result.records = await entity.CountAsync();
                if (filter.Limit < 10) filter.Limit = 10;
                if (filter.Offset < 0) filter.Offset = 0;
                result.rows = await entity.AsNoTracking().Skip(filter.Offset).Take(filter.Limit).ToListAsync();
            }
            return result;
        }

        public async Task<Dictionary<string, string>> GetSearchNameForAreaTextAsync(string keyWord)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                return await dbContext.Departments.AsNoTracking().Where(x => x.FullName.Contains(keyWord)).ToDictionaryAsync(x => x.Id, x => x.Name);
            }
        }
    }
}