using Logicore.IServices;
using Logicore.IRepository;
using System.Threading.Tasks;
using Logicore.Core.ViewModel;
using Logicore.Core.ServerModels;

namespace Logicore.Services
{
    /// <summary>
    /// 系统配置服务
    /// </summary>
    public class SystemConfigService : ISystemConfigService
    {
        private readonly ISystemConfigRepository _systemConfigRepository;

        /// <summary>
        /// ctor
        /// </summary>
        public SystemConfigService(ISystemConfigRepository systemConfigRepository)
        {
            _systemConfigRepository = systemConfigRepository;
        }
        public async Task<bool> EditAsync(SystemConfigDto dto)
        {
            return await _systemConfigRepository.UpdateAsync(dto);
        }

        public async Task<SystemConfigViewModel> GetAsync()
        {
            var entity = await _systemConfigRepository.GetAsync();
            SystemConfigViewModel viewModel = new SystemConfigViewModel()
            {
                SystemName = entity.SystemName,
                IsDataInited = entity.IsDataInited,
                DataInitedDate = entity.DataInitedDate.ToShortDateString()
            };
            return viewModel;
        }
    }
}