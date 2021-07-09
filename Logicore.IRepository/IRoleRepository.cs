using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Logicore.Core.Enities;
using Logicore.Core.Enities.ServiceModel;
using Logicore.Core.Filters;
using Logicore.Core.ServerModels;
using Logicore.Core.ViewModel;

namespace Logicore.IRepository
{
    /// <summary>
    /// 角色仓储接口
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="whereLambda">条件过滤</param>
        /// <param name="orderByLambda">排序过滤</param>
        /// <returns></returns>
        Task<PageResult<RoleEntity>> GetAsync(Expression<Func<RoleEntity, bool>> whereLambda, BaseFilter filter);

        /// <summary>
        /// 查询角色总数
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync();

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="dto">菜单模型</param>
        /// <returns></returns>
        Task<string> AddAsync(RoleEntity entity);

        /// <summary>
        /// 查找角色
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        Task<RoleEntity> FindAsync(string id);

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="dto">角色数据</param>
        /// <returns></returns>
        Task<bool> UpdataAsync(RoleDto dto);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(IEnumerable<string> ids);

        /// <summary>
        /// 角色下拉框搜索
        /// </summary>
        /// <param name="filter"></param>
        Task<List<DropDownRoleViewModel>> DropDownRoleSearchAsync(DropDownRoleFilter filter);

        /// <summary>
        /// 获取角色限制下菜单Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<List<string>> GetMenuIdWithRole(string roleId);

        /// <summary>
        /// 修改角色限权
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="ids">权限组</param>
        /// <returns></returns>
        Task<bool> EditPermissionWithRoleAsync(string roleId, IEnumerable<string> ids);
    }
}