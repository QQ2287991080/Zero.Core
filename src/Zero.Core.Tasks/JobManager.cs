using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Zero.Core.Tasks.Base;
using Zero.Core.Tasks.Jobs;

namespace Zero.Core.Tasks
{
    public class JobManager : IExample
    {
        readonly IJobFactory _jobFactory;
        private Task<IScheduler> _scheduler;
        public static List<string> jobKeys = new List<string>();
        public JobManager(IJobFactory jobFactory)
        {
            _jobFactory = jobFactory;
            _scheduler = CreateScheduler();
        }

        private IScheduler Scheduler => _scheduler.Result;
        public  Task<IScheduler> CreateScheduler()
        {
            if (_scheduler != null)
                return this._scheduler;
            else
            {
                //配置二进制策略
                var properties = new NameValueCollection
                {
                    ["quartz.serializer.type"] = "binary"
                };

                //实例化工厂
                ISchedulerFactory sf = new StdSchedulerFactory(properties);
                this._scheduler = sf.GetScheduler();
                return _scheduler;
            };
        }
        public async Task Run()
        {
            var jobKey = new JobKey("job1", "group1");

            if (await Scheduler.CheckExists(jobKey))
            {
                Console.WriteLine("JobKey  Exists");
            }
            else
            {
                Console.WriteLine("JobKey Allow");
                if (!this.Scheduler.IsStarted)
                {
                    Console.WriteLine("quartz is not  started");
                    await this.Start();
                }
                var job = JobBuilder.Create<OneJob>()
                   .WithIdentity("job1", "group1")
                   .Build();
                var trigger = TriggerBuilder.Create()
                   .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(a =>
                    {
                        a.RepeatForever();
                        a.WithIntervalInSeconds(3);
                    })
                    .ForJob(job)
                    .Build();

                await Scheduler.ScheduleJob(job, trigger);

                //var schedulerFactory = new StdSchedulerFactory();
                //var scheduler = await schedulerFactory.GetScheduler();
                //await scheduler.Start();
                //Console.WriteLine($"任务调度器已启动");

                ////创建作业和触发器
                //var jobDetail = JobBuilder.Create<OneJob>().Build();
                //var trigger = TriggerBuilder.Create()
                //                            .WithSimpleSchedule(m =>
                //                            {
                //                                m.WithRepeatCount(3).WithIntervalInSeconds(1);
                //                            })
                //                            .Build();

                ////ObjectUtils.SetPropertyValue();
                ////添加调度
                //await scheduler.ScheduleJob(jobDetail, trigger);
            }
        }

        public async Task Start()
        {

            try
            {
                //替换内置任务工厂
                Scheduler.JobFactory = this._jobFactory;
                if (this.Scheduler.IsStarted)
                {
                    Console.WriteLine("quartz is  started");

                }
                else
                {
                    Console.WriteLine("quartz start!");
                    await Scheduler.Start();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task PauseJob()
        {
            JobKey job = new JobKey("job1", "group1");
            if (await Scheduler.CheckExists(job))
            {
                await Scheduler.PauseJob(job);
            }
            else
            {
                Console.WriteLine("JobKey Not Exists");
                
            }
        }

        public async Task ResumeJob()
        {
            JobKey job = new JobKey("job1", "group1");
            if (await Scheduler.CheckExists(job))
            {
                await Scheduler.ResumeJob(job);
            }
            else
            {
                Console.WriteLine("JobKey Not Exists");
            }
        }

    }
}
