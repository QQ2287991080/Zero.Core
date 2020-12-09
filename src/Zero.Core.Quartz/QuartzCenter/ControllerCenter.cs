using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities;

namespace Zero.Core.Quartz.QuartzCenter
{
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
        public async Task<string> Start()
        {
            string message = "";
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
                    message += "启动成功";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                message += ex.Message;
            }
            return message;
        }

        public async Task<string> RunJob(Jobs jobs)
        {
            string message = "";

            if (jobs == null)
            {
                message += "参数对象不能为空。";
                return message;
            }

            //实例化job类
            var basePath = AppContext.BaseDirectory + jobs.AssemblyName;
            var assembly = Assembly.LoadFrom(basePath);
            var type = assembly.GetTypes()
             .First(f => f.Name == jobs.ClassName);
            if (type == null)
            {
                message += "Job对象不能为空。";
                return message;
            }
            //具体job对象
            //var job = Activator.CreateInstance(type) as IJob;
            //创建jobkey
            var jobKey = new JobKey(jobs.JobKey, jobs.JobGroup);
            //创建triggerKey
            var triggerKey = new TriggerKey(jobs.TriggerKey, jobs.JobGroup);
            if (await Scheduler.CheckExists(jobKey))
            {
                message += "JobKey  Exists";
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine("JobKey Allow");
                if (!this.Scheduler.IsStarted)
                {
                    Console.WriteLine("quartz is not  started");
                    await this.Start();
                }
                var jobDetails = JobBuilder.Create(type)
                   .WithIdentity(jobKey)
                   .Build();

                //触发器
                var trigger = TriggerBuilder.Create()
                    .WithIdentity(triggerKey);
                {
                    //启动时间
                    if (jobs.StartTime == null)
                        trigger.StartNow();
                    else
                    {
                        DateTimeOffset offset = new DateTimeOffset((DateTime)jobs.StartTime);
                        trigger.StartAt(offset);
                    }
                }
                {
                    trigger.WithSimpleSchedule(a =>
                    {
                        a.WithIntervalInSeconds(3);//执行间隔3s
                        a.RepeatForever();
                    });

                    //trigger.WithCronSchedule("", a =>
                    //{

                    //});
                }
                //结束时间
                {
                    //DateTimeOffset? endTime = jobs.EndTime ==null? new DateTimeOffset?() : new DateTimeOffset((DateTime)jobs.EndTime);
                    //trigger.EndAt(endTime);
                }

                message += $"启动任务【{jobs.Name}】成功！";
                // .Build();
                await Scheduler.ScheduleJob(jobDetails, trigger.Build());
            }
            return message;
        }

        public async Task<string> PauseJob(Jobs jobs)
        {
            string message = "";
            var jobKey = new JobKey(jobs.JobKey, jobs.JobGroup);
            if (await Scheduler.CheckExists(jobKey))
            {
                await Scheduler.PauseJob(jobKey);
                message += $"暂停【{jobs.Name}】 成功!";
                Console.WriteLine(message);
            }
            else
            {
                message += "Not IsExists JobKey";
                Console.WriteLine(message);
            }
            return message;
        }

        public async Task<string> ResumeJob(Jobs jobs)
        {
            string message = "";
            var jobKey = new JobKey(jobs.JobKey, jobs.JobGroup);
            if (await Scheduler.CheckExists(jobKey))
            {
                await Scheduler.ResumeJob(jobKey);
                message += $"恢复【{jobs.Name}】 成功!";
                Console.WriteLine(message);
            }
            else
            {
                message += "Not IsExists JobKey";
                Console.WriteLine(message);
            }
            return message;
        }
    }
}
