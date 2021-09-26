using System;
using System.Threading.Tasks;

namespace Logicore.Core.Quartz
{
    public interface IScheduleCenter
    {
        /// <summary>
        /// 添加调度任务
        /// 如StarTime为空时，默认为马上启用
        /// EndTime为空时，或小于开始时间，任务将永远被执行
        /// </summary>
        /// <param name="JobName">任务名称</param>
        /// <param name="JobGroup">任务分组</param>
        /// <param name="JobNamespaceAndClassName">任务完全限定名</param>
        /// <param name="JobAssemblyName">任务程序集路径及名称，已引用程序集可为不填</param>
        /// <param name="CronExpress">Cron表达式</param>
        /// <param name="StarTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="Description">任务描述</param>
        /// <returns>result.ResultCode，result.ResultMsg</returns>
        Task<ScheduleResult> AddJobAsync(string JobName, string JobGroup, string JobNamespaceAndClassName,
                    string CronExpress, string JobAssemblyName = null, DateTime? StarTime = null, DateTime? EndTime = null,
                    string Description = null);

        /// <summary>
        /// 暂停指定任务计划
        /// </summary>
        /// <param name="jobName">任务名</param>
        /// <param name="jobGroup">任务分组</param>
        /// <returns></returns>
        Task<ScheduleResult> StopJobAsync(string jobName, string jobGroup);

        /// <summary>
        /// 恢复指定的任务计划,如果是程序奔溃后 或者是进程杀死后的恢复，此方法无效
        /// </summary>
        /// <param name="jobName">任务名称</param>
        /// <param name="jobGroup">任务组</param>
        /// <returns></returns>
        Task<ScheduleResult> ResumeJobAsync(string jobName, string jobGroup);

        /// <summary>
        /// 删除指定的任务
        /// </summary>
        /// <param name="jobName">任务名称</param>
        /// <param name="jobGroup">任务组</param>
        /// <returns></returns>
        Task<ScheduleResult> DeleteJobAsync(string jobName, string jobGroup);

        /// <summary>
        /// 查看任务状态
        /// </summary>
        /// <param name="jobName">任务名称</param>
        /// <param name="jobGroup">任务组</param>
        /// <returns></returns>
        Task<ScheduleResult> CheckStatusAsync(string jobName, string jobGroup);
    }
}