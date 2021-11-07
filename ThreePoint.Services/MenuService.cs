using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ThreePoint.Core.Enities;
using ThreePoint.Core.Enities.ServiceModel;
using ThreePoint.Core.Enums;
using ThreePoint.Core.ServerModels;
using ThreePoint.Core.ViewModel;
using ThreePoint.IRepository;
using ThreePoint.IServices;
using ThreePoint.Core.Extensions;
using ThreePoint.Core.Filters;

namespace ThreePoint.Services
{
    /// <summary>
    /// 菜单服务
    /// </summary>
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="menuRepository">菜单仓储服务</param>
        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<string> AddAsync(MenuDto dto)
        {
            var str = await _menuRepository.AddAsync(dto);
            return str;
        }

        public async Task<bool> DeleteAsync(IEnumerable<string> ids)
        {
            var ok = await _menuRepository.DeleteAsync(ids);
            return ok;
        }

        public async Task<List<DropDownMenuViewModel>> DropDownMenuSearchAsync(DropDownMenuFilter filter)
        {
            if (filter.type == null) return null;
            var dropDownMenuList = await _menuRepository.DropDownMenuSearchAsync(filter);
            return dropDownMenuList;
        }

        public async Task<MenuDto> FindAsync(string id)
        {
            var menu = await _menuRepository.FindAsync(id);
            return menu;
        }

        public async Task<List<MenuDto>> GetMenusByRoleIdAsync(string roleId)
        {
            var menu = await _menuRepository.GetMenusByRoleIdAsync(roleId);
            return menu;
        }

        public async Task<PageResult<MenuTableViewModel>> GetMenuForTableAsync(MenuFilter filters)
        {
            Expression<Func<MenuEntity, bool>> exp = x => x.IsDeleted == false;
            if (filters.SearchMenuId.IsBlank() || filters.SearchModuleId.IsBlank())
            {
                if (filters.IncludeType != null)
                {
                    if (filters.IncludeType == filters.ExcludeType) return null;
                    exp = exp.And(x => x.Type == filters.IncludeType);
                }
                if (filters.ExcludeType != null)
                {
                    exp = exp.And(x => x.Type != filters.ExcludeType);
                }
            }
            if (filters.IsEnable != null)
            {
                exp = exp.And(x => x.Enabled == filters.IsEnable);
            }
            if (filters.SearchModuleId.IsNotBlank())
            {
                if (filters.SearchMenuId.IsBlank()) exp = exp.And(x => x.ParentId == filters.SearchModuleId);
                else exp = exp.And(x => x.ParentId == filters.SearchMenuId);
            }
            else
            {
                if (filters.SearchMenuId.IsNotBlank())
                {
                    exp = exp.And(x => x.ParentId == filters.SearchMenuId);
                }
            }
            if (filters.Keywords.IsNotBlank())
            {
                exp = exp.And(x => x.Name.Contains(filters.Keywords) || x.Url.Contains(filters.Keywords));
            }

            Expression<Func<MenuEntity, int>> orderExp = x => x.Order;
            BaseFilter filter = filters;
            var menuResult = await _menuRepository.GetAsync(exp, orderExp, filter);

            //将数据数据转换为视图数据
            List<MenuTableViewModel> menuTables = new List<MenuTableViewModel>();
            foreach (var item in menuResult.rows)
            {
                var model = new MenuTableViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    url = item.Url,
                    order = item.Order,
                    Icon = item.Icon,
                    Type = item.Type,
                    Enable = item.Enabled
                };
                if (item.ParentId.IsNotBlank()) model.ParentName = (await _menuRepository.FindAsync(item.ParentId)).ParentName;
                menuTables.Add(model);
            }

            PageResult<MenuTableViewModel> result = new PageResult<MenuTableViewModel>()
            {
                records = menuResult.records,
                rows = menuTables
            };
            return result;
        }

        public async Task<List<MenuDto>> GetMyMenusAsync(string userId, bool supuerManager = false)
        {
            var menus = await _menuRepository.GetMyMenusAsync(userId, supuerManager);
            return menus;
        }

        public async Task<bool> ReInitMenuesAsync(List<MenuDto> list)
        {
            var ok = await _menuRepository.ReInitMenuesAsync(list);
            return ok;
        }

        public async Task<bool> UpdateAsync(MenuDto dto)
        {
            var ok = await _menuRepository.UpdateAsync(dto);
            return ok;
        }

        public Task<int> GetCountAsync()
        {
            return _menuRepository.GetCountAsync();
        }
    }
}