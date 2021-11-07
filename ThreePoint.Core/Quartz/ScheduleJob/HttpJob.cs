using System;
using System.Threading.Tasks;
using Quartz;

namespace ThreePoint.Core.Quartz.ScheduleJob
{
    public class HttpJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            //更具任务组执行具体的任务
            //MyLogger.WriteMessage("gogogo");
            await Console.Out.WriteLineAsync("Greetings from HelloJob!");
        }
    }
}