using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ThreePoint.Core.Enities;
using ThreePoint.Core.Enities.ServiceModel;
using ThreePoint.Core.Filters;

namespace ThreePoint.IRepository
{
    public interface IServerExceptionRepository
    {
        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="entity">实体信息</param>
        /// <returns></returns>
        Task AddAsync(ServerExceptionEntity entity);

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="whereLambda">条件</param>
        /// <param name="filter">过滤</param>
        /// <returns></returns>
        Task<PageResult<ServerExceptionEntity>> GetAsync(Expression<Func<ServerExceptionEntity, bool>> whereLambda, BaseFilter filter);
    }
}