using System.Collections.Generic;
using System.Threading.Tasks;
using Logicore.Core.Enities;
using Logicore.IServices;
using Logicore.IRepository;

namespace Logicore.Services
{
    public class ScheduleInfoService : IScheduleInfoService
    {
        private readonly IScheduleInfoRepository _scheduleInfoRepository;

        public ScheduleInfoService(IScheduleInfoRepository scheduleInfoRepository)
        {
            _scheduleInfoRepository = scheduleInfoRepository;
        }
        public Task<bool> CreateAsync(ScheduleInfoEntity dto)
        {
            return _scheduleInfoRepository.CreateAsync(dto);
        }

        public Task<bool> DeleteAsync(string id)
        {
            return _scheduleInfoRepository.DeleteAsync(id);
        }

        public Task<List<ScheduleInfoEntity>> GetAllAsync()
        {
            return _scheduleInfoRepository.GetAllAsync();
        }

        public Task<ScheduleInfoEntity> GetAsync(string id)
        {
            return _scheduleInfoRepository.GetAsync(id);
        }
    }
}