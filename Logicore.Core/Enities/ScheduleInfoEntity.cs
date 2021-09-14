using System;
using System.ComponentModel;

namespace Logicore.Core.Enities
{
    public class ScheduleInfoEntity : BaseEntity
    {
        /// <summary>
        /// 任务组
        /// </summary>
        /// <value></value>
        [DisplayName("任务组")]
        public string JobGroup { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        /// <value></value>
        [DisplayName("JobName")]
        public string JobName { get; set; }

        /// <summary>
        /// 运行状态
        /// </summary>
        /// <value></value>
        [DisplayName("运行状态")]
        public int RunStatus { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        /// <value></value>
        [DisplayName("Cron表达式")]
        public string CromExpress { get; set; }

        /// <summary>
        /// 开始运行时间
        /// </summary>
        /// <value></value>
        [DisplayName("开始运行时间")]
        public DateTime? StarRunTime { get; set; }

        /// <summary>
        /// 结束运行时间
        /// </summary>
        /// <value></value>
        [DisplayName("结束运行时间")]
        public DateTime? EndRunTime { get; set; }

        /// <summary>
        /// 下次运行时间
        /// </summary>
        /// <value></value>
        [DisplayName("下次运行时间")]
        public DateTime? NextRunTime { get; set; }

        /// <summary>
        /// token
        /// </summary>
        /// <value></value>
        public string Token { get; set; }

        /// <summary>
        /// AppId
        /// </summary>
        /// <value></value>
        public string AppId { get; set; }

        /// <summary>
        /// 服务代码
        /// </summary>
        /// <value></value>
        public string ServiceCode { get; set; }

        /// <summary>
        /// 接口代码
        /// </summary>
        /// <value></value>
        public string InterfaceCode { get; set; }

        /// <summary>
        /// 任务描述
        /// </summary>
        /// <value></value>
        public string TaskDescription { get; set; }

        /// <summary>
        /// 数据状态
        /// </summary>
        /// <value></value>
        [DisplayName("数据状态")]
        public int? DataStatus { get; set; }
    }
}