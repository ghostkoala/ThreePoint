using System.Collections.Generic;
using System.Threading.Tasks;
using ThreePoint.Core.Enities.ServiceModel;
using ThreePoint.Core.Filters;
using ThreePoint.Core.ServerModels;
using ThreePoint.Core.ViewModel;

namespace ThreePoint.IServices
{
    /// <summary>
    /// 角色服务接口
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// 表格查找角色
        /// </summary>
        /// <param name="filter">过滤</param>
        /// <returns></returns>
        Task<PageResult<RoleTableViewModel>> GetRoleForTableAsync(RoleFilter filter);

        /// <summary>
        /// 共有角色总数
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync();

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="dto">菜单模型</param>
        /// <returns></returns>
        Task<string> AddAsync(RoleDto dto);

        /// <summary>
        /// 查找角色
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        Task<RoleDto> FindAsync(string id);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(IEnumerable<string> ids);

        /// <summary>
        /// 角色下拉选择搜索
        /// </summary>
        /// <param name="filter">过滤</param>
        /// <returns></returns>
        Task<List<DropDownRoleViewModel>> DropDownRoleSearchAsync(DropDownRoleFilter filter);

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="dto">角色数据</param>
        /// <returns></returns>
        Task<bool> EditAsync(RoleDto dto);

        /// <summary>
        /// 获取限制树
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        Task<List<ZtreeViewModel>> GetPermissionTreeAsync(string roleId);

        /// <summary>
        /// 修改角色限权
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="ids">权限组</param>
        /// <returns></returns>
        Task<bool> EditPermissionWithRoleAsync(string roleId, IEnumerable<string> ids);
    }
}