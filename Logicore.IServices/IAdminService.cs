using System.Collections.Generic;
using System.Threading.Tasks;
using Logicore.Core.Enities.ServiceModel;
using Logicore.Core.Filters;
using Logicore.Core.ServerModels;
using Logicore.Core.ViewModel;

namespace Logicore.IServices
{
    /// <summary>
    /// 管理员服务接口
    /// </summary>
    public interface IAdminService
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="dto">登录信息</param>
        /// <returns></returns>
        Task<ResultModel<AdminTableViewModel>> LoginAsync(LoginViewModel dto);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="dto">密码信息</param>
        /// <returns></returns>
        Task<bool> EditPassWord(AdminEditPassWordDto dto);

        /// <summary>
        /// 修改基本信息
        /// </summary>
        /// <param name="dto">用户数据</param>
        /// <returns></returns>
        Task<bool> EditGeneralInfoAsync(AdminGeneralInfoDto dto);

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
        /// 管理员查找-表格
        /// </summary>
        /// <param name="filter">过滤</param>
        /// <returns></returns>
        Task<PageResult<AdminTableViewModel>> GetAdminForTableAsync(AdminFilter filter);

        /// <summary>
        /// 查找管理员
        /// </summary>
        /// <param name="id">管理员</param>
        /// <returns></returns>
        Task<AdminDto> FindAsync(string id);

        /// <summary>
        /// 修改管理员信息
        /// </summary>
        /// <param name="dto">管理员数据</param>
        /// <returns></returns>
        Task<bool> EditAsync(AdminDto dto);

        /// <summary>
        /// 添加管理员信息
        /// </summary>
        /// <param name="dto">管理员数据</param>
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