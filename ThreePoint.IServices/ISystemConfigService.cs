using System.Threading.Tasks;
using ThreePoint.Core.ServerModels;
using ThreePoint.Core.ViewModel;

namespace ThreePoint.IServices
{
    /// <summary>
    /// 系统配置服务接口
    /// </summary>
    public interface ISystemConfigService
    {
        /// <summary>
        /// 更新系统配置信息
        /// </summary>
        /// <returns></returns>
        Task<bool> EditAsync(SystemConfigDto dto);

        /// <summary>
        /// 获取系统配置信息
        /// </summary>
        /// <returns></returns>
        Task<SystemConfigViewModel> GetAsync();
    }
}