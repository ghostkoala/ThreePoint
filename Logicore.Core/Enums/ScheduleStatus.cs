using System.ComponentModel;

namespace Logicore.Core.Enums
{
    public enum ScheduleStatus
    {
        [Description("已创建")]
        Created = 0,

        [Description("等待激活")]
        WaitingForActivation = 1,

        [Description("等待执行")]
        WaitingToRun = 2,

        [Description("运行中")]
        Running = 3,

        [Description("等待子任务完成")]
        WaitingForChildrenToComplete = 4,

        [Description("运行完成")]
        RanToCompletion = 5,

        [Description("取消")]
        Canceled = 6,

        [Description("错误的")]
        Faulted = 7
    }
}
}