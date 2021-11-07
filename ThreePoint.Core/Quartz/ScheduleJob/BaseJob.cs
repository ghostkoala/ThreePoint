using System.Threading.Tasks;
using Quartz;

namespace ThreePoint.Core.Quartz.ScheduleJob
{
    public class BaseJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}