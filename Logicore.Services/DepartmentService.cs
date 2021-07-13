using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Logicore.Core.Enities;
using Logicore.Core.Enities.ServiceModel;
using Logicore.Core.Exceptions;
using Logicore.Core.Extensions;
using Logicore.Core.Filters;
using Logicore.Core.ServerModels;
using Logicore.Core.ViewModel;
using Logicore.IRepository;
using Logicore.IServices;

namespace Logicore.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<bool> AddAsync(DepartmentDto dto)
        {
            return await _departmentRepository.AddAsync(dto);
        }

        public Task<bool> DeleteAsync(IEnumerable<string> ids)
        {
            return _departmentRepository.DeleteAsync(ids);
        }

        public async Task<List<DropDownDepartmentViewModel>> DropDownDepartmentSeachAsync(DropDownDepartmentFilter filter)
        {
            return await _departmentRepository.DropDownDepartmentSeachAsync(filter);
        }

        public async Task<bool> EditAsync(DepartmentDto dto)
        {
            return await _departmentRepository.EditAsync(dto);
        }

        public async Task<DepartmentDto> FindAsync(string id)
        {
            var entity = await _departmentRepository.FindAsync(id);
            if (entity == null) throw new BusinessException("无此部门", 401);
            var dto = new DepartmentDto()
            {
                Id = id,
                Name = entity.Name,
                ParentId = entity.ParentId,
                Enabled = entity.Enable
            };
            return dto;
        }

        public async Task<PageResult<DepartmentTableViewModel>> GetDepartmentForTableAsync(DepartmentFilter filter)
        {
            Expression<Func<DepartmentEntity, bool>> exp = x => x.IsDeleted == false;
            if (filter.Name.IsNotBlank()) exp = exp.And(x => x.Name.Contains(filter.Name));
            if (filter.ParentId.IsNotBlank()) exp = exp.And(x => x.ParentId == filter.ParentId);
            if (filter.Enabled != null) exp = exp.And(x => x.Enable == filter.Enabled);
            var rows = await _departmentRepository.GetAsync(exp, filter);
            List<DepartmentTableViewModel> departments = new List<DepartmentTableViewModel>();
            foreach (var item in rows.rows)
            {
                departments.Add(new DepartmentTableViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    FullName = item.FullName,
                    ParentId = item.ParentId,
                    Enable = item.Enable,
                    CreateTime = item.CreateDateTime.ToString("f")
                });
            }
            PageResult<DepartmentTableViewModel> result = new PageResult<DepartmentTableViewModel>()
            {
                records = rows.records,
                rows = departments
            };
            return result;
        }

        public async Task<Dictionary<string, string>> GetSearchNameForAreaTextAsync(string keyWord)
        {
            return await _departmentRepository.GetSearchNameForAreaTextAsync(keyWord);
        }
    }
}