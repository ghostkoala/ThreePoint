using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logicore.Core.ServerModels;
using Logicore.IRepository;
using Logicore.Repository.DbContextService;
using Microsoft.EntityFrameworkCore;

namespace Logicore.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IDbContextFactory _dbContextFactory;

        public MessageRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<int> GetMyMessageCountAsync(string userId)
        {
            var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read);
            var query = dbContext.MessageReceivers.Where(x => x.IsReaded == false && x.UserId == userId);
            return await query.CountAsync();
        }

        public async Task<IList<MessageQueryDto>> GetUnReadMesasgeAsync(string userId)
        {
            var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read);
            var query = from message in dbContext.Messages
                        join receiver in dbContext.MessageReceivers on message.Id equals receiver.MessageId into receivers
                        from receiver in receivers.DefaultIfEmpty()
                        where receiver.IsReaded == false && receiver.UserId == userId
                        select new MessageQueryDto
                        {
                            Id = message.Id,
                            Title = message.Title,
                            CreateDateTime = message.CreateDateTime
                        };
            return await query.ToListAsync();
        }
    }
}