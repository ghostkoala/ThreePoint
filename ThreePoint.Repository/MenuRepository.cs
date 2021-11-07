using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ThreePoint.Core.Enities;
using ThreePoint.Core.Enities.ServiceModel;
using ThreePoint.Core.Enums;
using ThreePoint.Core.Exceptions;
using ThreePoint.Core.Extensions;
using ThreePoint.Core.Filters;
using ThreePoint.Core.ServerModels;
using ThreePoint.Core.ViewModel;
using ThreePoint.IRepository;
using ThreePoint.Repository.DbContextService;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ThreePoint.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly IPathCodeRepository _pathCodeRepository;
        private readonly ILogger<MenuRepository> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dbContextFactory">数据库工厂</param>
        /// <param name="pathCodeRepository">路径码服务</param>
        public MenuRepository(IDbContextFactory dbContextFactory,
        IPathCodeRepository pathCodeRepository,
        ILogger<MenuRepository> logger,
        IHttpContextAccessor httpContextAccessor)
        {
            _dbContextFactory = dbContextFactory;
            _pathCodeRepository = pathCodeRepository;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> AddAsync(MenuDto dto)
        {
            var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write);
            var pathCodeDbSet = _pathCodeRepository.GetPathCodes();
            MenuEntity entity = dto;
            entity.CreateBy(_httpContextAccessor.HttpContext.Session.GetString("Uid"));
            var dbSet = dbContext.Menus;
            var isExists = await dbContext.Menus.CountAsync(x => x.Id == entity.Id);
            if (isExists > 0) throw new BusinessException("已存在此菜单");
            if (entity.Type == MenuType.Module) entity.Url = "#";
            if (entity.Type == MenuType.Menu)
            {
                var fatherMenu = await dbContext.Menus.FirstOrDefaultAsync(x => x.Id == entity.ParentId);
                if (fatherMenu.Type == MenuType.Menu) throw new BusinessException("不支持添加多级菜单");
            }

            var existsCode = await dbSet.Where(item => item.ParentId == dto.ParentId)
                    .Select(item => item.Code).ToListAsync();
            var pathCode = pathCodeDbSet.FirstOrDefault(item => !existsCode.Contains(item));
            entity.Code = pathCode.Trim();
            if (entity.ParentId.IsNotBlank())
            {
                var parent = await dbSet.FirstOrDefaultAsync(m => m.Id == entity.ParentId);
                entity.PathCode = string.Concat(parent.PathCode.Trim(), entity.Code.Trim());
            }
            else
            {
                entity.PathCode = entity.Code.Trim();
                entity.Type = MenuType.Module;
            }
            dbSet.Add(entity);

            await dbContext.SaveChangesAsync();
            return entity.Id;
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
                        var entities = dbContext.Menus.Where(x => ids.Contains(x.Id));
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

        public async Task<List<DropDownMenuViewModel>> DropDownMenuSearchAsync(DropDownMenuFilter filter)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                List<DropDownMenuViewModel> dropDownMenuList = new List<DropDownMenuViewModel>();
                if (filter.model == DropDownOrSelectModel.DropDown)
                {
                    if (filter.type == MenuType.Module) return null;
                    Expression<Func<MenuEntity, bool>> whereExp;
                    if (filter.type == MenuType.Menu) whereExp = x => x.Type == MenuType.Module;
                    else whereExp = x => x.Type == MenuType.Menu;
                    var menu = await dbContext.Menus.Where(whereExp).Select(x => new { x.Id, x.Name, x.Type, x.ParentId }).ToListAsync();
                    foreach (var item in menu)
                    {
                        dropDownMenuList.Add(new DropDownMenuViewModel()
                        {
                            Id = item.Id,
                            Name = item.Name
                        });
                    }
                }
                else
                {
                    Expression<Func<MenuEntity, bool>> whereExp;
                    whereExp = x => x.Type == filter.type;
                    if (filter.menuId.IsNotBlank() && filter.type != MenuType.Action)
                    {
                        Expression<Func<MenuEntity, bool>> secondExp;
                        secondExp = x => x.ParentId == filter.menuId;
                        whereExp = whereExp.And(secondExp);
                    }
                    var menu = await dbContext.Menus.Where(whereExp).Select(x => new { x.Id, x.Name, x.Type, x.ParentId }).ToListAsync();
                    foreach (var item in menu)
                    {
                        dropDownMenuList.Add(new DropDownMenuViewModel()
                        {
                            Id = item.Id,
                            Name = item.Name
                        });
                    }
                }
                return dropDownMenuList;
            }
        }

        public async Task<MenuDto> FindAsync(string id)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                var entity = await dbContext.Menus.FirstOrDefaultAsync(m => m.Id == id);
                MenuDto dto = entity;
                if (dto.ParentId.IsNotBlank())
                {
                    var parent = await dbContext.Menus.FirstOrDefaultAsync(m => m.Id == dto.ParentId);
                    dto.ParentName = parent.Name;
                }
                return dto;
            }
        }

        public async Task<List<MenuEntity>> GetAllAsync()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                return await dbContext.Menus.ToListAsync();
            }
        }

        public async Task<PageResult<MenuEntity>> GetAsync(Expression<Func<MenuEntity, bool>> whereLambda, Expression<Func<MenuEntity, int>> orderByLambda, BaseFilter filter)
        {
            if (filter == null) filter = new BaseFilter();
            var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read);
            IOrderedQueryable<ThreePoint.Core.Enities.MenuEntity> menu;
            if (filter.Order == "asc") menu = dbContext.Menus.Where(whereLambda).OrderBy(orderByLambda);
            else menu = dbContext.Menus.Where(whereLambda).OrderByDescending(orderByLambda);
            PageResult<MenuEntity> result = new PageResult<MenuEntity>();
            result.records = await menu.CountAsync();
            if (filter.Limit < 10) filter.Limit = 10;
            if (filter.Offset < 0) filter.Limit = 0;
            result.rows = await menu.Skip(filter.Offset).Take(filter.Limit).ToListAsync();
            return result;
        }

        public async Task<int> GetCountAsync()
        {
            int i = 0;
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                i = await dbContext.Menus.CountAsync();
            }
            return i;
        }

        public async Task<List<MenuDto>> GetMenusByRoleIdAsync(string roleId)
        {
            var dtos = new List<MenuDto>();
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                var list = await dbContext.Menus
                .Join(dbContext.RoleMenus, m => m.Id, rm => rm.MenuId, (menu, roleMenu) => new { menu, roleMenu })
                .Where(item => item.roleMenu.RoleId == roleId)
                .Select(item => item.menu).ToListAsync();
                foreach (var item in list)
                {
                    dtos.Add(item);
                }
            }
            return dtos;
        }

        public async Task<List<MenuDto>> GetMyMenusAsync(string userId, bool supuerManager)
        {
            var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read);
            var user = await dbContext.Admins.FirstOrDefaultAsync(x => x.Id == userId && x.Enabled == true);
            if (user == null)
            {
                return new List<MenuDto>();
            }

            var dbSetRoleMenus = dbContext.RoleMenus;
            var menuIds = dbSetRoleMenus.Where(x => x.RoleId == user.RoleId)
                .Select(x => x.MenuId);
            //如果是超级管理员,则默认有所有的权限
            Expression<Func<MenuEntity, bool>> exp = x => x.Type != MenuType.Action;
            if (!supuerManager) exp = exp.And(x => menuIds.Contains(x.Id));

            return await dbContext.Menus.Where(exp)
                .Select(x => new MenuDto
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    Name = x.Name,
                    Url = x.Url,
                    Order = x.Order,
                    Type = (MenuType)x.Type,
                    Icon = x.Icon
                }).ToListAsync();
        }

        public async Task<bool> ReInitMenuesAsync(List<MenuDto> list)
        {
            if (!list.AnyOne())
            {
                return false;
            }

            var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write);

            //删除以前所有的菜单和菜单的角色关系
            var oldMenues = await dbContext.Menus.ToListAsync();
            oldMenues.ForEach(x => x.IsDeleted = true);
            var oldMenuRoles = await dbContext.RoleMenus.ToListAsync();
            dbContext.RoleMenus.RemoveRange(oldMenuRoles);
            //删除已经存在的相同id的菜单
            var ids = list.Select(x => x.Id).Distinct().ToList();
            var sameIdMenus = await dbContext.Menus.Where(x => ids.Contains(x.Id)).ToListAsync();
            dbContext.Menus.RemoveRange(sameIdMenus);
            await dbContext.SaveChangesAsync();
            //重置新的菜单
            var moduleIds = list.Where(x => x.ParentId.IsBlank()).Select(x => x.Id);
            List<MenuEntity> menues = new List<MenuEntity>();
            list.ForEach(x => menues.Add(x));
            foreach (var menu in menues)
            {
                //设置菜单类型
                if (menu.ParentId.IsBlank())
                {
                    menu.Type = MenuType.Module;
                }
                else if (moduleIds.Contains(menu.ParentId))
                {
                    menu.Type = MenuType.Menu;
                }
                else
                {
                    menu.Type = MenuType.Action;
                }

                //设置菜单的路径(层级关系) 父类的PathCode+当前类别的Id
                var parent = menues.FirstOrDefault(x => x.Id == menu.ParentId);
                menu.Code = menu.Id;
                if (parent == null)
                {
                    menu.PathCode = menu.Id;
                }
                else
                {
                    menu.PathCode = $"{parent.PathCode}-{menu.Id}";
                }
                if (menu.Url.IsBlank())
                {
                    menu.Url = "#";
                }
                menu.CreateDateTime = DateTime.Now;
            }
            dbContext.Menus.AddRange(menues);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(MenuDto dto)
        {
            var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write);
            var strategy = dbContext.Database.CreateExecutionStrategy();
            var ok = true;
            await strategy.Execute(async () =>
            {
                using (var trans = await dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var entity = dbContext.Menus.FirstOrDefault(x => x.Id == dto.Id);
                        entity.Name = dto.Name;
                        entity.Url = dto.Url;
                        entity.Order = dto.Order;
                        entity.Icon = dto.Icon;
                        entity.Type = dto.Type;
                        await dbContext.SaveChangesAsync();
                        await trans.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "更新菜单失败");
                        await trans.RollbackAsync();
                        ok = false;
                    }
                }
            });
            return ok;
        }
    }
}