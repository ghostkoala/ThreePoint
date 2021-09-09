using System.Threading.Tasks;
using Logicore.Core.ServerModels;
using Logicore.Core.Enities;

namespace Logicore.IRepository
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