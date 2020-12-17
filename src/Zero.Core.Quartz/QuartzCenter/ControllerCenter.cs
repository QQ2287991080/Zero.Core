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
using Zero.Core.Domain.Enums;

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

        public async Task<string> RunJob(Jobs jobInfo)
        {
            string message = "";

            if (jobInfo == null)
            {
                message += "参数对象不能为空。";
                return message;
            }

            //实例化job类
            var basePath = AppContext.BaseDirectory + jobInfo.AssemblyName;
            var assembly = Assembly.LoadFrom(basePath);
            var type = assembly.GetTypes()
             .First(f => f.Name == jobInfo.ClassName);
            if (type == null)
            {
                message += "Job对象不能为空。";
                return message;
            }
            //具体job对象
            //var job = Activator.CreateInstance(type) as IJob;
            //创建jobkey
            var jobKey = new JobKey(jobInfo.JobKey, jobInfo.JobGroup);
            //创建triggerKey
            var triggerKey = new TriggerKey(jobInfo.TriggerKey, jobInfo.JobGroup);
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

                if (jobInfo.TriggerType == TriggerType.cron && !string.IsNullOrEmpty(jobInfo.CronExpression))
                {
                    trigger = CronBuilder(trigger, jobInfo);
                }
                else
                {
                    trigger = SimpleBuilder(trigger, jobInfo);
                }
                //启动时间
                {
                    if (jobInfo.StartTime == null)
                        trigger.StartNow();
                    else
                    {
                        DateTimeOffset offset = new DateTimeOffset((DateTime)jobInfo.StartTime);
                        trigger.StartAt(offset);
                    }
                }
                //结束时间
                {
                    trigger.EndAt(jobInfo.EndTime);
                }
                trigger.ForJob(jobKey);
                message += $"启动任务【{jobInfo.Name}】成功！";
                // .Build();
                await Scheduler.ScheduleJob(jobDetails, trigger.Build());
            }
            return message;
        }

        public async Task<string> PauseJob(Jobs jobInfo)
        {
            string message = "";
            var jobKey = new JobKey(jobInfo.JobKey, jobInfo.JobGroup);
            if (await Scheduler.CheckExists(jobKey))
            {
                await Scheduler.PauseJob(jobKey);
                message += $"暂停【{jobInfo.Name}】 成功!";
                Console.WriteLine(message);
            }
            else
            {
                message += "Not IsExists JobKey";
                Console.WriteLine(message);
            }
            return message;
        }

        public async Task<string> ResumeJob(Jobs jobInfo)
        {
            string message = "";
            var jobKey = new JobKey(jobInfo.JobKey, jobInfo.JobGroup);
            if (await Scheduler.CheckExists(jobKey))
            {
                await Scheduler.ResumeJob(jobKey);
                message += $"恢复【{jobInfo.Name}】 成功!";
                Console.WriteLine(message);
            }
            else
            {
                message += "Not IsExists JobKey";
                Console.WriteLine(message);
            }
            return message;
        }



        private TriggerBuilder SimpleBuilder(TriggerBuilder builder, Jobs jobInfo)
        {
            builder.WithSimpleSchedule(s =>
            {
                var interval = jobInfo.TriggerInterval;
                var count = jobInfo.Interval;
                if (interval == TriggerInterval.seconds)
                    s.WithIntervalInSeconds(count);
                else if (interval == TriggerInterval.minutes)
                    s.WithIntervalInMinutes(count);
                else
                    s.WithIntervalInHours(count);
                s.RepeatForever();
                //哑火策略,当前默认
                s.WithMisfireHandlingInstructionNextWithExistingCount();

            });
            return builder;     
        }

        private TriggerBuilder CronBuilder(TriggerBuilder builder,Jobs jobInfo)
        {
            //MisfireInstruction.CalendarIntervalTrigger.DoNothing;
            string cron = jobInfo.CronExpression;
            builder.WithCronSchedule(cron);
            //https://www.cnblogs.com/yaopengfei/p/8542771.html
            return builder;
        }

    }
}
