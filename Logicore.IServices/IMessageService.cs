using System.Collections.Generic;
using System.Threading.Tasks;
using Logicore.Core.Enities.ServiceModel;
using Logicore.Core.Filters;
using Logicore.Core.ServerModels;
using Logicore.Core.ViewModel;

namespace Logicore.IServices
{
    /// <summary>
    /// 信息服务接口
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// 获取我的未读消息统计
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        Task<int> GetMyMessageCountAsync(string userId);

        /// <summary>
        /// 获取我的未读消息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        Task<IList<MessageQueryViewModel>> GetUnReadMesasgeAsync(string userId);

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="dto">数据</param>
        /// <returns></returns>
        Task<bool> SendAsync(MessageDto dto);

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="dto">数据</param>
        /// <returns></returns>
        Task<bool> EditAsync(MessageDto dto);

        /// <summary>
        /// Ids
        /// </summary>
        /// <param name="ids">Ids</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(IEnumerable<string> ids);

        /// <summary>
        /// 查找站内信
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        Task<MessageDto> FindAsync(string id);

        /// <summary>
        /// 站内信查找-表格
        /// </summary>
        /// <param name="filter">过滤</param>
        /// <returns></returns>
        Task<PageResult<MessageQueryViewModel>> GetMessageForTableAsync(MessageFilter filter);

        /// <summary>
        /// 获取站内信详细信息
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        Task<MessageQueryViewModel> GetMessageDetails(string id);
    }
}