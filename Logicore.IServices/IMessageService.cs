using System.Collections.Generic;
using System.Threading.Tasks;
using Logicore.Core.ServerModels;

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
        Task<IList<MessageQueryDto>> GetUnReadMesasgeAsync(string userId);
    }
}