using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.IServices;

namespace Zero.Core.Quartz.Job
{
    [DisallowConcurrentExecution]
    [PersistJobDataAfterExecution]
    public class FirstJob : IJob
    {
        readonly IUserService _user;
        readonly IJobService _job;
        public FirstJob(IUserService user, IJobService job)
        {
            _user = user;
            _job = job;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(async () =>
            {
                Console.WriteLine("First Job");
                var userCount = _user.Query().Count();
                Console.WriteLine($"用户数量！{userCount}");
                //job详情
                var jobDetails = context.JobDetail;
                //触发器的信息
                var trigger = context.Trigger;
                string keyName = jobDetails.Key.Name;
                Console.WriteLine($"JobKey：{keyName}，Group：{jobDetails.Key.Group}\r\n" +
                    $"Trigger：{trigger.Key}\r\n" +
                    $"RunTime：{context.JobRunTime}\r\n" +
                    $"ExecuteTime：{DateTime.Now}");

                var job = await _job.FirstAsync(w => w.Id == Convert.ToInt32(jobDetails.Key.Name));
                if (job != null)
                {
                    job.ExecuteCount++;
                    job.LastTime = DateTime.Now;
                    await _job.UpdateAsync(job);
                }
            });
        }
    }
}
