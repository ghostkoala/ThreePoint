using System.Collections.Generic;
using System.Threading.Tasks;
using Logicore.Core.ServerModels;
using Logicore.IRepository;
using Logicore.IServices;

namespace Logicore.Services
{
    /// <summary>
    /// 信息服务
    /// </summary>
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<int> GetMyMessageCountAsync(string userId)
        {
            var count = await _messageRepository.GetMyMessageCountAsync(userId);
            return count;
        }

        public async Task<IList<MessageQueryDto>> GetUnReadMesasgeAsync(string userId)
        {
            var message = await _messageRepository.GetUnReadMesasgeAsync(userId);
            return message;
        }
    }
}