using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Logicore.Core.Enities;
using Logicore.Core.Enities.ServiceModel;
using Logicore.Core.Extensions;
using Logicore.Core.Filters;
using Logicore.Core.ServerModels;
using Logicore.Core.ViewModel;
using Logicore.IRepository;
using Logicore.IServices;

namespace Logicore.Services
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public class RoleService : IRoleService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IRoleRepository _roleRepository;

        /// <summary>
        /// ctor
        /// </summary>
        public RoleService(IRoleRepository roleRepository, IAdminRepository adminRepository, IMenuRepository menuRepository)
        {
            _adminRepository = adminRepository;
            _menuRepository = menuRepository;
            _roleRepository = roleRepository;
        }

        public Task<string> AddAsync(RoleDto dto)
        {
            RoleEntity role = new RoleEntity();
            role.Init();
            role.Name = dto.Name;
            role.Description = dto.Description;
            role.Enabled = dto.Enabled;
            return _roleRepository.AddAsync(role);
        }

        public Task<bool> DeleteAsync(IEnumerable<string> ids)
        {
            return _roleRepository.DeleteAsync(ids);
        }

        public async Task<List<DropDownRoleViewModel>> DropDownRoleSearchAsync(DropDownRoleFilter filter)
        {
            return await _roleRepository.DropDownRoleSearchAsync(filter);
        }

        public async Task<bool> EditAsync(RoleDto dto)
        {
            return await _roleRepository.UpdataAsync(dto);
        }

        public async Task<bool> EditPermissionWithRoleAsync(string roleId, IEnumerable<string> ids)
        {
            return await _roleRepository.EditPermissionWithRoleAsync(roleId, ids);
        }

        public async Task<RoleDto> FindAsync(string id)
        {
            return await _roleRepository.FindAsync(id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _roleRepository.GetCountAsync();
        }

        public async Task<List<ZtreeViewModel>> GetPermissionTreeAsync(string roleId)
        {
            var menus = await _menuRepository.GetAllAsync();
            var mIds = await _roleRepository.GetMenuIdWithRole(roleId);
            var result = new List<ZtreeViewModel>();
            foreach (var item in menus)
            {
                var ztree = new ZtreeViewModel()
                {
                    Id = item.Id,
                    PId = item.ParentId,
                    Name = item.Name
                };
                if (mIds.Contains(item.Id)) ztree.Checked = true;
                else ztree.Checked = false;
                result.Add(ztree);
            }
            return result;
        }

        public async Task<PageResult<RoleTableViewModel>> GetRoleForTableAsync(RoleFilter filters)
        {
            Expression<Func<RoleEntity, bool>> exp = x => x.IsDeleted == false;
            if (filters.SearchDescription.IsNotBlank()) exp = exp.And(x => x.Description.Contains(filters.SearchDescription));
            if (filters.SearchName.IsNotBlank()) exp = exp.And(x => x.Name.Contains(filters.SearchName));
            var entity = await _roleRepository.GetAsync(exp, filters);
            List<RoleTableViewModel> roles = new List<RoleTableViewModel>();
            foreach (var item in entity.rows)
            {
                var Admin = await _adminRepository.FindAsync(item.Id);
                roles.Add(new RoleTableViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Enabled = item.Enabled,
                    Description = item.Description,
                    CreateDateTime = item.CreateDateTime.ToString("f"),
                    CreateUser = Admin != null ? Admin.RealName : ""
                });
            }
            PageResult<RoleTableViewModel> result = new PageResult<RoleTableViewModel>()
            {
                rows = roles,
                records = entity.records
            };
            return result;
        }

        public Task<bool> UpdataAsync(RoleDto dto)
        {
            return _roleRepository.UpdataAsync(dto);
        }
    }
}