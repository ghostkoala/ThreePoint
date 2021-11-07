using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ThreePoint.Core.Enities;
using ThreePoint.Core.Enities.ServiceModel;
using ThreePoint.Core.Exceptions;
using ThreePoint.Core.Extensions;
using ThreePoint.Core.Filters;
using ThreePoint.Core.ServerModels;
using ThreePoint.Core.ViewModel;
using ThreePoint.IRepository;
using ThreePoint.IServices;

namespace ThreePoint.Services
{
    /// <summary>
    /// 信息服务
    /// </summary>
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository; ;
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

        public async Task<MessageQueryViewModel> GetMessageDetails(string id)
        {
            var result = await _messageRepository.GetMessageDetails(id);
            var detail = new List<MessageDetailViewModel>();
            foreach (var item in result.MessageReceivers)
            {
                detail.Add(new MessageDetailViewModel()
                {
                    UserId = item.Id,
                    IsReaded = item.IsReaded,
                    UserName = item.Admin.RealName,
                    ReadDate = item.ReadDate
                });
            }
            var messageDetail = new MessageQueryViewModel()
            {
                Id = result.Id,
                Title = result.Title,
                Contents = result.Contents,
                CreateDateTime = result.CreateDateTime.ToString("f"),
                ReadedNumber = result.ReadedNumber,
                Total = result.Total,
                Details = detail
            };
            return messageDetail;
        }

        public async Task<PageResult<MessageQueryViewModel>> GetMessageForTableAsync(MessageFilter filter)
        {
            Expression<Func<MessageEntity, bool>> exp = x => x.IsDeleted == false;
            var list = new PageResult<MessageEntity>();
            if (filter.SearchUserId.IsNotBlank())
            {
                list = await _messageRepository.FindUserAsync(filter.SearchUserId, filter.SearchTitle, filter);
            }
            else
            {
                if (filter.SearchTitle.IsNotBlank()) exp = exp.And(x => x.Title.Contains(filter.SearchTitle));
                Expression<Func<MessageEntity, DateTime>> orderExp = x => x.CreateDateTime;
                list = await _messageRepository.GetAsync(exp, orderExp, filter);
            }
            var result = new PageResult<MessageQueryViewModel>();
            result.records = list.records;
            foreach (var item in list.rows)
            {
                result.rows.Add(new MessageQueryViewModel()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Contents = item.Contents,
                    Total = item.Total,
                    CreateDateTime = item.CreateDateTime.ToString("f"),
                    ReadedNumber = item.ReadedNumber,
                });
            }
            return result;
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

        public async Task<ReceiverMessageViewModel> ReadMessageAsync(string id)
        {
            var message = await _messageRepository.ReadMessageAsync(id);
            if (message == null) return null;
            return new ReceiverMessageViewModel()
            {
                Title = message.Message.Title,
                Contents = message.Message.Contents,
                IsReaded = message.IsReaded,
                FirstReadDate = message.ReadDate?.ToString("f"),
                CreateDateTime = message.CreateDateTime.ToString("f")
            };
        }

        public Task<bool> SendAsync(MessageDto dto)
        {
            if (dto.IsToAll == false)
            {
                if (dto.ReceiverIds.Count() <= 0) throw new BusinessException("无接收者", 403);
            }
            if (dto.Title.IsBlank() || dto.Contents.IsBlank()) throw new BusinessException("输入的信息不全", 404);
            return _messageRepository.SendAsync(dto);
        }
    }
}