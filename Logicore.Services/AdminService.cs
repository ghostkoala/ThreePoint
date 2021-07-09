using System.Collections.Generic;
using System.Threading.Tasks;
using Logicore.Core.Enities.ServiceModel;
using Logicore.Core.ServerModels;
using Logicore.Core.ViewModel;
using Logicore.IRepository;
using Logicore.IServices;
using Logicore.Core.Extensions;
using Logicore.Core.Filters;
using System.Linq.Expressions;
using System;
using Logicore.Core.Enities;
using System.Linq;

namespace Logicore.Services
{
    /// <summary>
    /// 管理员服务
    /// </summary>
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="adminRepository"></param>
        /// <param name="mapper"></param>
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<bool> AddAsync(AdminDto dto)
        {
            return await _adminRepository.AddAsync(dto);
        }

        public async Task<bool> CheckPermitAsync(string roleId, string url)
        {
            return await _adminRepository.CheckPermitAsync(roleId, url);
        }

        public async Task<bool> DeleteAsync(IEnumerable<string> ids)
        {
            return await _adminRepository.DeleteAsync(ids);
        }

        public async Task<bool> EditAsync(AdminDto dto)
        {
            return await _adminRepository.UpdataAsync(dto);
        }

        public async Task<bool> EditGeneralInfoAsync(AdminGeneralInfoDto dto)
        {
            if (dto.Password.IsNotBlank()) dto.Password = dto.Password.ToMd5();
            return await _adminRepository.UpdataGeneralInfoAsync(dto);
        }

        public async Task<bool> EditPassWord(AdminEditPassWordDto dto)
        {
            dto.NewPassWord = dto.NewPassWord.ToMd5();
            dto.OldPassWord = dto.OldPassWord.ToMd5();
            return await _adminRepository.UpdataPassWordAsync(dto);
        }

        public async Task<AdminDto> FindAsync(string id)
        {
            var entity = await _adminRepository.FindAsync(id);
            AdminDto dto = new AdminDto();

            dto.Id = entity.Id;
            dto.LoginName = entity.LoginName;
            dto.RealName = entity.RealName;
            dto.Email = entity.Email ?? "";
            dto.Enabled = entity.Enabled;
            dto.Password = "";
            dto.DepartmentId = entity.DepartmentId ?? "";
            dto.DepartmentName = entity.Department != null ? entity.Department.Name : "";
            dto.RoleId = entity.RoleId ?? "";
            dto.RoleName = entity.Role != null ? entity.Role.Name : "";
            return dto;
        }

        public async Task<AdminGeneralInfoDto> FindGeneralInfoAsync(string id)
        {
            return await _adminRepository.FindGeneralInfoAsync(id);
        }

        public async Task<PageResult<AdminTableViewModel>> GetAdminForTableAsync(AdminFilter filter)
        {
            Expression<Func<AdminEntity, bool>> exp = x => x.IsDeleted == false;
            if (filter.SearchLoginName.IsNotBlank()) exp = exp.And(x => x.LoginName.Contains(filter.SearchLoginName));
            if (filter.SearchRealName.IsNotBlank()) exp = exp.And(x => x.RealName.Contains(filter.SearchRealName));
            if (filter.IsEnabled != null) exp = exp.And(x => x.Enabled == filter.IsEnabled);
            if (filter.SearchDepartmentId.IsNotBlank()) exp = exp.And(x => x.Department.Id == filter.SearchDepartmentId);
            if (filter.SearchRoleId.IsNotBlank()) exp = exp.And(x => x.RoleId.Contains(filter.SearchRoleId));
            var rows = await _adminRepository.GetAsync(exp, filter);
            List<AdminTableViewModel> admins = new List<AdminTableViewModel>();
            foreach (var item in rows.rows)
            {
                admins.Add(new AdminTableViewModel()
                {
                    Id = item.Id,
                    LoginName = item.LoginName,
                    RealName = item.RealName,
                    Email = item.Email,
                    CreateDateTime = item.CreateDateTime.ToString("f"),
                    DepartmentName = item.Department != null ? item.Department.Name : null,
                    RoleId = item.RoleId,
                    RoleName = item.Role != null ? item.Role.Name : null,
                    Enable = item.Enabled
                });
            }

            PageResult<AdminTableViewModel> result = new PageResult<AdminTableViewModel>()
            {
                records = rows.records,
                rows = admins
            };
            return result;
        }

        public Task<int> GetCountAsync()
        {
            return _adminRepository.GetCountAsync();
        }

        public async Task<ResultModel<AdminTableViewModel>> LoginAsync(LoginViewModel dto)
        {
            dto.Password = dto.Password.ToMd5();
            var result = await _adminRepository.LoginAsync(dto);
            return result;
        }
    }
}