using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ThreePoint.Core.Enities;
using ThreePoint.Core.Enities.ServiceModel;
using ThreePoint.Core.Extensions;
using ThreePoint.Core.Exceptions;
using ThreePoint.Core.Filters;
using ThreePoint.Core.ServerModels;
using ThreePoint.Core.ViewModel;
using ThreePoint.IRepository;
using ThreePoint.Repository.DbContextService;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ThreePoint.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAdminRepository _adminRepository;
        private readonly ILogger<MessageRepository> _logger;

        public MessageRepository(IDbContextFactory dbContextFactory,
            IHttpContextAccessor httpContextAccessor,
            IAdminRepository adminRepository,
            ILogger<MessageRepository> logger)
        {
            _dbContextFactory = dbContextFactory;
            _httpContextAccessor = httpContextAccessor;
            _adminRepository = adminRepository;
            _logger = logger;
        }

        public async Task<bool> DeleteAsync(IEnumerable<string> ids)
        {
            var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write);
            var strategy = dbContext.Database.CreateExecutionStrategy();
            var ok = true;
            await strategy.Execute(async () =>
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var entities = dbContext.Messages.Where(x => ids.Contains(x.Id));
                        dbContext.RemoveRange(entities);
                        var i = await dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                        if (i <= 0) ok = false;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "?????????????????????");
                        await transaction.RollbackAsync();
                        ok = false;
                    }
                }
            });
            return ok;
        }

        public async Task<bool> UpdataAsync(MessageDto dto)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write))
            {
                var entity = await dbContext.Messages.Include(x => x.MessageReceivers).FirstOrDefaultAsync(x => x.Id == dto.Id);
                if (entity == null) return false;
                entity.Title = dto.Title;
                entity.Contents = dto.Contents;
                if (entity.MessageReceivers.Count() > 0)
                {
                    foreach (var item in entity.MessageReceivers)
                    {
                        item.IsReaded = false;
                    }
                }
                dbContext.Update(entity);
                var i = await dbContext.SaveChangesAsync();
                return i <= 0 ? false : true;
            }
        }

        public async Task<int> GetMyMessageCountAsync(string userId)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                var query = dbContext.MessageReceivers.Where(x => x.IsReaded == false && x.UserId == userId);
                return await query.CountAsync();
            }
        }

        public async Task<IList<MessageQueryViewModel>> GetUnReadMesasgeAsync(string userId)
        {
            var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read);
            var query = from message in dbContext.Messages
                        join receiver in dbContext.MessageReceivers on message.Id equals receiver.MessageId into receivers
                        from receiver in receivers.DefaultIfEmpty()
                        where receiver.IsReaded == false && receiver.UserId == userId
                        select new MessageQueryViewModel
                        {
                            Id = message.Id,
                            Title = message.Title,
                            CreateDateTime = message.CreateDateTime.ToString("f")
                        };
            return await query.ToListAsync();
        }

        public async Task<bool> SendAsync(MessageDto dto)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write))
            {
                var message = new MessageEntity();
                message.CreateBy(_httpContextAccessor.HttpContext.Session.GetString("Uid"));
                message.Title = dto.Title;
                message.Contents = dto.Contents;
                var receiver = new MessageReceiverEntity();
                if (dto.IsToAll)
                {
                    var Ids = await dbContext.Admins.Where(x => x.IsDeleted == false).Select(x => x.Id).ToListAsync();
                    if (Ids.Count() <= 0) return false;
                    else message.Total = Ids.Count();
                    foreach (var item in Ids)
                    {
                        receiver.Init();
                        receiver.UserId = item;
                        receiver.MessageId = message.Id;
                        message.MessageReceivers.Add(receiver);
                    }
                }
                else
                {
                    if (dto.ReceiverIds.Count() <= 0) return false;
                    message.Total = dto.ReceiverIds.Count();
                    foreach (var item in dto.ReceiverIds)
                    {
                        if (item.IsBlank())
                        {
                            message.Total -= 1;
                            continue;
                        }
                        if (await _adminRepository.IsExist(item))
                        {
                            receiver.Init();
                            receiver.UserId = item;
                            receiver.MessageId = message.Id;
                            message.MessageReceivers.Add(receiver);
                        }
                        else
                        {
                            message.Total -= 1;
                        }
                    }
                }
                await dbContext.Messages.AddAsync(message);
                var i = await dbContext.SaveChangesAsync();
                return i <= 0 ? false : true;
            }
        }

        public async Task<MessageEntity> FindAsync(string id)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                return await dbContext.Messages.AsNoTracking().Include(x => x.MessageReceivers).FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<PageResult<MessageEntity>> GetAsync(Expression<Func<MessageEntity, bool>> whereLambda, Expression<Func<MessageEntity, DateTime>> orderByLambda, BaseFilter filter)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                var result = new PageResult<MessageEntity>();
                IOrderedQueryable<ThreePoint.Core.Enities.MessageEntity> messages;
                if (filter.Order == "asc") messages = dbContext.Messages.Where(whereLambda).OrderBy(orderByLambda);
                else messages = dbContext.Messages.Where(whereLambda).OrderByDescending(orderByLambda);
                result.records = await messages.CountAsync();
                if (filter.Limit < 10) filter.Limit = 10;
                if (filter.Offset < 0) filter.Limit = 0;
                result.rows = await messages.Skip(filter.Offset).Take(filter.Limit).ToListAsync();
                return result;
            }
        }

        public async Task<PageResult<MessageEntity>> FindUserAsync(string id, string title, BaseFilter filter)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                var receiver = dbContext.MessageReceivers.AsNoTracking().Include(x => x.Message).Where(x => x.UserId == id && x.Message.Title.Contains(title));
                var result = new PageResult<MessageEntity>();
                result.records = await receiver.CountAsync();
                if (filter.Limit < 10) filter.Limit = 10;
                if (filter.Offset < 0) filter.Limit = 0;
                result.rows = await receiver.Skip(filter.Offset).Take(filter.Limit).Select(x => x.Message).ToListAsync();
                return result;
            }
        }

        public async Task<MessageEntity> GetMessageDetails(string id)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Read))
            {
                return await dbContext.Messages.Include(x => x.MessageReceivers).ThenInclude(x => x.Admin).AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<MessageReceiverEntity> ReadMessageAsync(string id)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext(DbContextType.Write))
            {
                var uid = _httpContextAccessor.HttpContext.Session.GetString("Uid");
                var entity = await dbContext.MessageReceivers.Include(x => x.Message).FirstOrDefaultAsync(x => x.MessageId == id && x.UserId == uid);
                if (entity != null)
                {
                    if (!entity.IsDeleted)
                    {
                        entity.IsReaded = true;
                        entity.ReadDate = DateTime.Now;
                        dbContext.Update(entity);
                        await dbContext.SaveChangesAsync();
                    }
                }
                return entity;
            }
        }
    }
}