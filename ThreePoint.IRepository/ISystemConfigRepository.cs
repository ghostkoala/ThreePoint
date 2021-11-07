using System.Threading.Tasks;
using ThreePoint.Core.ServerModels;
using ThreePoint.Core.Enities;

namespace ThreePoint.IRepository
{
    /// <summary>
    /// 系统配置仓储
    /// </summary>
    public interface ISystemConfigRepository
    {
        /// <summary>
        /// 更新系统配置信息
        /// </summary>
        /// <returns></returns>
        Task<bool> UpdateAsync(SystemConfigDto dto);

        /// <summary>
        /// 获取系统配置
        /// </summary>
        /// <returns></returns>
        Task<SystemConfigEntity> GetAsync();
    }
}