using System.Collections.Generic;
using System.Threading.Tasks;
using Logicore.Core.Enities;
using Logicore.IRepository;
using Logicore.Repository.DbContextService;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;
using Logicore.Core.Exceptions;

namespace Logicore.Repository
{
    /// <summary>
    /// 任务仓储实现
    /// </summary>
    public class ScheduleInfoRepository : IScheduleInfoRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly ILogger<ScheduleInfoRepository> _logger;

        /// <summary>
        /// ctor
        /// </summary>
        public ScheduleInfoRepository(IDbContextFactory dbContextFactory, ILogger<ScheduleInfoRepository> logger)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
        }
        public async Task<bool> CreateAsync(ScheduleInfoEntity dto)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write))
            {
                await dbContext.scheduleInfoEntities.AddAsync(dto);
                var ok = await dbContext.SaveChangesAsync();
                return ok > 0 ? true : false;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write))
            {
                var entity = await dbContext.scheduleInfoEntities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (entity == null) throw new ServerException("无此任务，无法删除！", 404);
                dbContext.scheduleInfoEntities.Remove(entity);
                var ok = await dbContext.SaveChangesAsync();
                return ok > 0 ? true : false;
            }
        }

        public async Task<List<ScheduleInfoEntity>> GetAllAsync()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                return await dbContext.scheduleInfoEntities.AsNoTracking().ToListAsync();
            }
        }

        public Task<ScheduleInfoEntity> FindAsync(string id)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                return dbContext.scheduleInfoEntities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<bool> UpdataStatusAsync(string id, int status)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write))
            {
                var entity = await dbContext.scheduleInfoEntities.FirstOrDefaultAsync(x => x.Id == id);
                if (entity == null) throw new ServerException("无此任务，无法刷新状态！", 404);
                entity.RunStatus = status;
                dbContext.scheduleInfoEntities.Update(entity);
                var ok = await dbContext.SaveChangesAsync();
                return ok > 0 ? true : false;
            }
        }
    }
}