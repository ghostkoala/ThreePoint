using System.Collections.Generic;
using System.Threading.Tasks;
using ThreePoint.Core.Enities;

namespace ThreePoint.IRepository
{
    /// <summary>
    /// 任务信息仓储
    /// </summary>
    public interface IScheduleInfoRepository
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

        /// <summary>
        /// 更新任务状态
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        Task<bool> UpdataStatusAsync(string id, int status);
    }
}