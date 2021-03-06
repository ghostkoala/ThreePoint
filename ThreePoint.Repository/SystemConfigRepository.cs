using System.Threading.Tasks;
using ThreePoint.Core.Enities;
using ThreePoint.Core.ServerModels;
using ThreePoint.IRepository;
using ThreePoint.Repository.DbContextService;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ThreePoint.Repository
{
    public class SystemConfigRepository : ISystemConfigRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly ILogger<SystemConfigRepository> _logger;

        public SystemConfigRepository(IDbContextFactory dbContextFactory, ILogger<SystemConfigRepository> logger)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
        }

        public async Task<SystemConfigEntity> GetAsync()
        {
            using (var dbContextFactory = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                return await dbContextFactory.SystemConfigs.AsNoTracking().FirstOrDefaultAsync();
            }
        }

        public async Task<bool> UpdateAsync(SystemConfigDto dto)
        {
            using (var dbContextFactory = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                var entity = await dbContextFactory.SystemConfigs.FirstOrDefaultAsync();
                entity.SystemName = dto.SystemName;
                dbContextFactory.SystemConfigs.Update(entity);
                var i = await dbContextFactory.SaveChangesAsync();
                return i > 0 ? true : false;
            }
        }
    }
}