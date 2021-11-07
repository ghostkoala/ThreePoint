using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ThreePoint.Core.Enities;
using ThreePoint.Core.Filters;
using ThreePoint.IRepository;
using ThreePoint.Repository.DbContextService;
using System.Linq;
using ThreePoint.Core.Enities.ServiceModel;
using Microsoft.EntityFrameworkCore;

namespace ThreePoint.Repository
{
    public class ServerExceptionRepository : IServerExceptionRepository
    {
        private readonly IDbContextFactory _dbContextFactory;

        public ServerExceptionRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task AddAsync(ServerExceptionEntity entity)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write))
            {
                await dbContext.serverExceptions.AddAsync(entity);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<PageResult<ServerExceptionEntity>> GetAsync(Expression<Func<ServerExceptionEntity, bool>> whereLambda, BaseFilter filter)
        {
            PageResult<ServerExceptionEntity> result = new PageResult<ServerExceptionEntity>();
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                var errList = dbContext.serverExceptions.Where(whereLambda);
                result.records = await errList.CountAsync();

                if (filter.Limit < 10) filter.Limit = 10;
                if (filter.Offset < 0) filter.Offset = 0;
                result.rows = await errList.Skip(filter.Offset).Take(filter.Limit).ToListAsync();
            }
            return result;
        }
    }
}