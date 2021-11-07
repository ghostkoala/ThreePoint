using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ThreePoint.Core.Enities;
using ThreePoint.Core.Enities.ServiceModel;
using ThreePoint.Core.Filters;
using ThreePoint.Core.ServerModels;
using ThreePoint.Core.ViewModel;

namespace ThreePoint.IRepository
{
    /// <summary>
    /// 管理员仓储接口
    /// </summary>
    public interface IAdminRepository
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="dto">登录信息</param>
        /// <returns></returns>
        Task<ResultModel<AdminTableViewModel>> LoginAsync(LoginViewModel dto);

        /// <summary>
        /// 根据Id查找用户
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <returns></returns>
        Task<AdminEntity> FindAsync(string id);

        /// <summary>
        /// 修改基本信息
        /// </summary>
        /// <param name="dto">用户数据</param>
        /// <returns></returns>
        Task<bool> UpdataGeneralInfoAsync(AdminGeneralInfoDto dto);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdataPassWordAsync(AdminEditPassWordDto dto);

        /// <summary>
        /// 获取管理员基本信息
        /// </summary>
        /// <param name="id">管理员Id</param>
        /// <returns></returns>
        Task<AdminGeneralInfoDto> FindGeneralInfoAsync(string id);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(IEnumerable<string> ids);

        /// <summary>
        /// 查找管理员总数
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync();

        /// <summary>
        /// 获取管理员
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<PageResult<AdminEntity>> GetAsync(Expression<Func<AdminEntity, bool>> whereLambda, BaseFilter filter);

        /// <summary>
        /// 修改管理员信息
        /// </summary>
        /// <param name="dto">管理员信息</param>
        /// <returns></returns>
        Task<bool> UpdataAsync(AdminDto dto);

        /// <summary>
        /// 添加管理员信息
        /// </summary>
        /// <param name="dto">管理员信息</param>
        /// <returns></returns>
        Task<bool> AddAsync(AdminDto dto);

        /// <summary>
        /// 访问允许检测
        /// </summary>
        /// <param name="roleId">角色</param>
        /// <param name="url">url</param>
        /// <returns></returns>
        Task<bool> CheckPermitAsync(string roleId, string url);


        /// <summary>
        /// 查找用户是否存在
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        Task<bool> IsExist(string id);

        /// <summary>
        /// 管理员下拉菜单
        /// </summary>
        /// <param name="filter">过滤</param>
        /// <returns></returns>
        Task<List<DropDownAdminViewModel>> DropDownAdminSeachAsync(DropDownAdminFilter filter);
    }
}