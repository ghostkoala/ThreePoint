using System.Collections.Generic;
using System.Threading.Tasks;
using Logicore.Core.ServerModels;

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
        Task<IList<MessageQueryDto>> GetUnReadMesasgeAsync(string userId);
    }
}