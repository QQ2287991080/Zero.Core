using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.Tasks.Jobs
{
    [DisallowConcurrentExecution]
    [PersistJobDataAfterExecution]
    public class OneJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var job = context.JobDetail;
            var trigger = context.Trigger;
            var executeCount = context.RefireCount;
            await Task.Run(() =>
            {
                Console.WriteLine($"JobKye：【{job.Key}】，TriggerKey：【{trigger.Key}】,执行次数：{executeCount},执行时间：{DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss fff")}");
            });
        }
    }
}
