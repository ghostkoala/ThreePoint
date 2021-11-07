using System.Collections.Generic;
using System.Threading.Tasks;
using ThreePoint.Core.Enities;
using ThreePoint.IServices;
using ThreePoint.IRepository;

namespace ThreePoint.Services
{
    public class ScheduleInfoService : IScheduleInfoService
    {
        private readonly IScheduleInfoRepository _scheduleInfoRepository;

        public ScheduleInfoService(IScheduleInfoRepository scheduleInfoRepository)
        {
            _scheduleInfoRepository = scheduleInfoRepository;
        }
        public async Task<bool> CreateAsync(ScheduleInfoEntity dto)
        {
            return await _scheduleInfoRepository.CreateAsync(dto);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _scheduleInfoRepository.DeleteAsync(id);
        }

        public async Task<List<ScheduleInfoEntity>> GetAllAsync()
        {
            return await _scheduleInfoRepository.GetAllAsync();
        }

        public async Task<ScheduleInfoEntity> FindAsync(string id)
        {
            return await _scheduleInfoRepository.FindAsync(id);
        }

        public async Task<bool> UpdataStatus(string id, int status)
        {
            return await _scheduleInfoRepository.UpdataStatusAsync(id, status);
        }
    }
}