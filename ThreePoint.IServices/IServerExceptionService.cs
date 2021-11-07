using System.Collections.Generic;
using System.Threading.Tasks;
using ThreePoint.Core.Enities;
using ThreePoint.Core.Enities.ServiceModel;
using ThreePoint.Core.Filters;
using ThreePoint.Core.ViewModel;

namespace ThreePoint.IServices
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
        /// 查找异常信息
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        Task<ServerExceptionEntity> FindAsync(string id);

        /// <summary>
        /// 表格查找角色
        /// </summary>
        /// <param name="filter">过滤</param>
        /// <returns></returns>
        Task<PageResult<ServerExceptionTableViewModel>> GetServerExceptionForTable(ServerExceptionFilter filter);
    }
}