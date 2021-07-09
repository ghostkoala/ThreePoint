using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Logicore.Core.Enities;
using Logicore.Core.Enities.ServiceModel;
using Logicore.Core.Filters;
using Logicore.IRepository;
using Logicore.IServices;
using Logicore.Core.Extensions;

namespace Logicore.Services
{
    /// <summary>
    /// 系统错误信息服务
    /// </summary>
    public class ServerExceptionService : IServerExceptionService
    {
        private readonly IServerExceptionRepository _serverExceptionRepository;

        /// <summary>
        /// ctor
        /// </summary>
        public ServerExceptionService(IServerExceptionRepository serverExceptionRepository)
        {
            _serverExceptionRepository = serverExceptionRepository;
        }
        public async Task AddAsync(ServerExceptionEntity entity)
        {
            await _serverExceptionRepository.AddAsync(entity);
        }

        public async Task<PageResult<ServerExceptionEntity>> FindAsync(ServerExceptionFilter filter)
        {
            Expression<Func<ServerExceptionEntity, bool>> exp = null;
            if (filter.StartTime != null)
                exp = exp.And(x => x.CreateTime >= filter.StartTime);
            if (filter.EndTime != null)
                exp = exp.And(x => x.CreateTime <= filter.EndTime);
            if (filter.category != null)
                exp = exp.And(x => x.errCategory == filter.category);

            return await _serverExceptionRepository.GetAsync(exp, filter);
        }
    }
}