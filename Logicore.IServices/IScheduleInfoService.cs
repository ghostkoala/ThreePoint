using System.Collections.Generic;
using System.Threading.Tasks;
using Logicore.Core.Enities;

namespace Logicore.IServices
{
    /// <summary>
    /// 任务信息服务
    /// </summary>
    public interface IScheduleInfoService
    {
        /// <summary>
        /// 获取所有任务
        /// </summary>
        /// <returns></returns>
        Task<List<ScheduleInfoEntity>> GetAllAsync();

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="dto">DTO</param>
        /// <returns></returns>
        Task<bool> CreateAsync(ScheduleInfoEntity dto);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string id);

        /// <summary>
        /// 获取任务
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task<ScheduleInfoEntity> FindAsync(string id);
    }
}