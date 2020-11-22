using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Quartz.Jobs;

namespace Zero.Core.Quartz.Base
{
    /// <summary>
    /// Quartz任务调度控制中心
    /// </summary>
    public class ControllerCenter : IControllerCenter
    {

        /// <summary>
        /// 构造函数注入自定义Job工厂
        /// </summary>
        readonly IJobFactory _jobFactory;
        private Task<IScheduler> _scheduler;

        public Task<IScheduler> Center => _scheduler ?? throw new ArgumentNullException("Schedule can not null");
        private IScheduler Scheduler => _scheduler.Result;
        public ControllerCenter(IJobFactory factory)
        {
            _jobFactory = factory;
            _scheduler = GetScheduler();
            _scheduler.Result.JobFactory = _jobFactory;
        }

        private Task<IScheduler> GetScheduler()
        {
            if (_scheduler != null)
                return _scheduler;
            else
            {
                /*
                 * 配置二进制策略
                 *https://www.quartz-scheduler.net/documentation/quartz-3.x/migration-guide.html#packaging-changes
                 */
                var properties = new NameValueCollection
                {
                    ["quartz.serializer.type"] = "binary"
                };
                //实例化工厂
                ISchedulerFactory sf = new StdSchedulerFactory(properties);
                this._scheduler = sf.GetScheduler();
                return _scheduler;
            }
        }
        public async Task Start()
        {
            try
            {
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
        public async Task RunJob()
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
                var job = JobBuilder.Create<FirstJob>()
                   .WithIdentity("job1", "group1")
                   .Build();
                var trigger = TriggerBuilder.Create()
                   .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(a =>
                    {
                        a.RepeatForever();//永远执行
                        a.WithIntervalInSeconds(3);//执行间隔3s
                    })
                    .ForJob(job)
                    .Build();

                await Scheduler.ScheduleJob(job, trigger);
            }
        }
        public async Task PauseJob()
        {
            var jobKey = new JobKey("job1", "group1");
            if (await Scheduler.CheckExists(jobKey))
            {
                await Scheduler.PauseJob(jobKey);
                Console.WriteLine("PauseJob Success!");
            }
            else
            {
                Console.WriteLine("Not IsExists JobKey");
            }
        }

        public async Task ResumeJob()
        {
            var jobKey = new JobKey("job1", "group1");
            if (await Scheduler.CheckExists(jobKey))
            {
                await Scheduler.ResumeJob(jobKey);
                Console.WriteLine("ResumeJob Success!");
            }
            else
            {
                Console.WriteLine("Not IsExists JobKey");
            }
        }
    }
}
