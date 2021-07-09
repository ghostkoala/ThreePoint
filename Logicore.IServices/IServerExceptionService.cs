using System.Collections.Generic;
using System.Threading.Tasks;
using Logicore.Core.Enities;
using Logicore.Core.Enities.ServiceModel;
using Logicore.Core.Filters;

namespace Logicore.IServices
{
    /// <summary>
    /// 系统错误信息服务接口
    /// </summary>
    public interface IServerExceptionService
    {
        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(ServerExceptionEntity entity);

        /// <summary>
        /// 查找信息
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<PageResult<ServerExceptionEntity>> FindAsync(ServerExceptionFilter filter);
    }
}