using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logicore.Core.Exceptions;
using Logicore.Core.Extensions;
using Logicore.Core.ServerModels;
using Logicore.Core.ViewModel;
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

        public Task<bool> DeleteAsync(IEnumerable<string> ids)
        {
            if (ids.Count() <= 0) throw new BusinessException("无要删除的数据", 403);
            return _messageRepository.DeleteAsync(ids);
        }

        public Task<bool> EditAsync(MessageDto dto)
        {
            if (dto.Title.IsBlank() || dto.Contents.IsBlank())
                throw new BusinessException("输入的数据有误", 403);
            return _messageRepository.UpdataAsync(dto);
        }

        public async Task<MessageDto> FindAsync(string id)
        {
            var entity = await _messageRepository.FindAsync(id);
            var dto = new MessageDto()
            {
                Id = entity.Id,
                Title = entity.Title,
                Contents = entity.Contents
            };
            if (entity.MessageReceivers.Count > 0)
            {
                dto.ReceiverIds = entity.MessageReceivers.Select(x => x.Id).ToList();
            }
            return dto;
        }

        public async Task<int> GetMyMessageCountAsync(string userId)
        {
            var count = await _messageRepository.GetMyMessageCountAsync(userId);
            return count;
        }

        public async Task<IList<MessageQueryViewModel>> GetUnReadMesasgeAsync(string userId)
        {
            var message = await _messageRepository.GetUnReadMesasgeAsync(userId);
            return message;
        }

        public Task<bool> SendAsync(MessageDto dto)
        {
            if (dto.ToAll == null || dto.ToAll == false)
            {
                if (dto.ReceiverIds.Count() <= 0) throw new BusinessException("无接收者", 403);
            }
            if (dto.Title.IsBlank() || dto.Contents.IsBlank()) throw new BusinessException("输入的信息不全", 404);
            return _messageRepository.SendAsync(dto);
        }
    }
}