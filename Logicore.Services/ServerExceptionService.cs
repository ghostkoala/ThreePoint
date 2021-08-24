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
using Logicore.Core.ViewModel;

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

        public Task<ServerExceptionEntity> FindAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<PageResult<ServerExceptionTableViewModel>> GetServerExceptionForTable(ServerExceptionFilter filter)
        {
            Expression<Func<ServerExceptionEntity, bool>> exp = null;
            if (filter.StartTime != null)
                exp = exp.And(x => x.CreateTime >= filter.StartTime);
            if (filter.EndTime != null)
                exp = exp.And(x => x.CreateTime <= filter.EndTime);
            if (filter.category != null)
                exp = exp.And(x => x.errCategory == filter.category);

            var exceptions = await _serverExceptionRepository.GetAsync(exp, filter);

            PageResult<ServerExceptionTableViewModel> result = new PageResult<ServerExceptionTableViewModel>();
            result.records = exceptions.records;
            var rows = new List<ServerExceptionTableViewModel>();
            foreach (var item in result.rows)
            {
                rows.Add(new ServerExceptionTableViewModel()
                {
                    Id = item.Id,
                    Code = item.Code,
                    Url = item.Url,
                    Method = item.Method,
                    ErrMessage = item.ErrMessage,
                    CreateTime = item.CreateTime
                });
            };
            result.rows = rows;
            return result;
        }
    }
}