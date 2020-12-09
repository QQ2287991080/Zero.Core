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
        public FirstJob(IUserService user)
        {
            _user = user;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() =>
            {
                Console.WriteLine("First Job");
                var userCount = _user.Query().Count();
                Console.WriteLine($"用户数量！{userCount}");
                //job详情
                var jobDetails = context.JobDetail;
                //触发器的信息
                var trigger = context.Trigger;
                Console.WriteLine($"JobKey：{jobDetails.Key}，Group：{jobDetails.Key.Group}\r\n" +
                    $"Trigger：{trigger.Key}\r\n" +
                    $"RunTime：{context.JobRunTime}\r\n" +
                    $"ExecuteTime：{DateTime.Now}");
            });
        }
    }
}
