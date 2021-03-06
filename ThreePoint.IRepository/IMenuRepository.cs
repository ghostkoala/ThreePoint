using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ThreePoint.Core.Enities;
using ThreePoint.Core.Enities.ServiceModel;
using ThreePoint.Core.Enums;
using ThreePoint.Core.Filters;
using ThreePoint.Core.ServerModels;
using ThreePoint.Core.ViewModel;

namespace ThreePoint.IRepository
{
    /// <summary>
    /// 菜单仓储接口
    /// </summary>
    public interface IMenuRepository
    {
        /// <summary>
        /// 重置系统的所有菜单
        /// </summary>
        /// <returns></returns>
        Task<bool> ReInitMenuesAsync(List<MenuDto> list);

        /// <summary>
        /// 获取所有菜单总数
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync();

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="whereLambda">条件过滤</param>
        /// <param name="orderByLambda">排序过滤</param>
        /// <param name="filter">基础过滤</param>
        /// <returns></returns>
        Task<PageResult<MenuEntity>> GetAsync(Expression<Func<MenuEntity, bool>> whereLambda, Expression<Func<MenuEntity, int>> orderByLambda, BaseFilter filter);

        /// <summary>
        /// 获取用户拥有的权限菜单（不包含按钮）
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        Task<List<MenuDto>> GetMyMenusAsync(string userId, bool supuerManager);

        /// <summary>
        /// 菜单下拉框搜索
        /// </summary>
        /// <param name="type">菜单类型</param>
        /// <returns></returns>
        Task<List<DropDownMenuViewModel>> DropDownMenuSearchAsync(DropDownMenuFilter filter);

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
        //Task<PagedResult<MenuDto>> SearchAsync(MenuFilter filters);

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        //Task<List<TreeDto>> GetTreesAsync();

        /// <summary>
        /// 通过角色ID获取拥有的菜单权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        Task<List<MenuDto>> GetMenusByRoleIdAsync(string roleId);

        /// <summary>
        /// 查找所有菜单
        /// </summary>
        /// <returns></returns>
        Task<List<MenuEntity>> GetAllAsync();
    }
}