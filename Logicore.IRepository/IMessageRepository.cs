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

        /// <summary>
        /// 获取站内信
        /// </summary>
        /// <param name="whereLambda">条件过滤</param>
        /// <param name="orderByLambda">排序过滤</param>
        /// <param name="filter">基础过滤</param>
        /// <returns></returns>
        Task<PageResult<MessageEntity>> GetAsync(Expression<Func<MessageEntity, bool>> whereLambda, Expression<Func<MessageEntity, DateTime>> orderByLambda, BaseFilter filter);

        /// <summary>
        /// 查找用户站内信
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="filter">基础过滤</param>
        /// <returns></returns>
        Task<PageResult<MessageEntity>> FindUserAsync(string id, string title, BaseFilter filter);

        /// <summary>
        /// 获取站内信详细信息
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        Task<MessageQueryViewModel> GetMessageDetails(string id);
    }
}