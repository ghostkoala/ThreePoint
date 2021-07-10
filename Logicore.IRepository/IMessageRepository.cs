using System.Collections.Generic;
using System.Threading.Tasks;
using Logicore.Core.Enities;
using Logicore.Core.ServerModels;
using Logicore.Core.ViewModel;

namespace Logicore.IRepository
{
    /// <summary>
    /// 信息仓储接口
    /// </summary>
    public interface IMessageRepository
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
        Task<bool> UpdataAsync(MessageDto dto);

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
        Task<MessageEntity> FindAsync(string id);
    }
}