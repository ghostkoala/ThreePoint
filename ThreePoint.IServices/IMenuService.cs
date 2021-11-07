using System.Collections.Generic;
using System.Threading.Tasks;
using ThreePoint.Core.Enities.ServiceModel;
using ThreePoint.Core.Enums;
using ThreePoint.Core.Filters;
using ThreePoint.Core.ServerModels;
using ThreePoint.Core.ViewModel;

namespace ThreePoint.IServices
{
    /// <summary>
    /// 菜单服务接口
    /// </summary>
    public interface IMenuService
    {
        /// <summary>
        /// 获取用户拥有的权限菜单（不包含按钮）
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="supuerManager">是否超级管理员<param>
        /// <returns></returns>
        Task<List<MenuDto>> GetMyMenusAsync(string userId, bool supuerManager = false);

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="dto">菜单模型</param>
        /// <returns></returns>
        Task<string> AddAsync(MenuDto dto);

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="dto">菜单模型</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(MenuDto dto);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(IEnumerable<string> ids);

        /// <summary>
        /// 根据主键查询模型
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<MenuDto> FindAsync(string id);

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="filters">查询过滤参数</param>
        /// <returns></returns>
        // Task<PagedResult<MenuDto>> SearchAsync(MenuFilter filters);

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        //Task<List<TreeDto>> GetTreesAsync();

        /// <summary>
        /// 菜单下拉框搜索
        /// </summary>
        /// <param name="type">菜单类型</param>
        /// <returns></returns>
        Task<List<DropDownMenuViewModel>> DropDownMenuSearchAsync(DropDownMenuFilter filter);

        /// <summary>
        /// 通过角色ID获取拥有的菜单权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        Task<List<MenuDto>> GetMenusByRoleIdAsync(string roleId);

        /// <summary>
        /// 重置系统的所有菜单
        /// </summary>
        /// <returns></returns>
        Task<bool> ReInitMenuesAsync(List<MenuDto> list);

        /// <summary>
        /// 搜索菜单-表格
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<PageResult<MenuTableViewModel>> GetMenuForTableAsync(MenuFilter filters);

        /// <summary>
        /// 获取所有菜单总数
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync();
    }
}