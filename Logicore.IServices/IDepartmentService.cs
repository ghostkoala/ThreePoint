using System.Collections.Generic;
using System.Threading.Tasks;
using Logicore.Core.Enities.ServiceModel;
using Logicore.Core.Filters;
using Logicore.Core.ServerModels;
using Logicore.Core.ViewModel;

namespace Logicore.IServices
{
    /// <summary>
    /// 部门服务接口
    /// </summary>
    public interface IDepartmentService
    {
        /// <summary>
        /// 部门下拉菜单
        /// </summary>
        /// <param name="filter">过滤</param>
        /// <returns></returns>
        Task<List<DropDownDepartmentViewModel>> DropDownDepartmentSeachAsync(DropDownDepartmentFilter filter);

        /// <summary>
        /// 部门查找-表格
        /// </summary>
        /// <param name="filter">过滤</param>
        /// <returns></returns>
        Task<PageResult<DepartmentTableViewModel>> GetDepartmentForTableAsync(DepartmentFilter filter);

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(IEnumerable<string> ids);

        /// <summary>
        /// 增加部门
        /// </summary>
        /// <param name="dto">数据</param>
        /// <returns></returns>
        Task<bool> AddAsync(DepartmentDto dto);

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="dto">数据</param>
        /// <returns></returns>
        Task<bool> EditAsync(DepartmentDto dto);

        /// <summary>
        /// 查找部门
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        Task<DepartmentDto> FindAsync(string id);

        /// <summary>
        /// 搜索部门id、名称
        /// </summary>
        /// <param name="keyWord">关键字</param>
        /// <returns></returns>
        Task<Dictionary<string, string>> GetSearchNameForAreaTextAsync(string keyWord);
    }
}