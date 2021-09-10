using System.Threading.Tasks;
using Quartz;

namespace Logicore.Core.Quartz.ScheduleJob
{
    public class BaseJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}