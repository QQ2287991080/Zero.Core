using Quartz;
using Quartz.Impl;
using Quartz.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Zero.Core.xUnitTest.Quartz
{
    public class QuartzNetTest
    {
        readonly ITestOutputHelper _ouput;
        static IScheduler _scheduler = null;

        const string TriggerStr = "mytrigger";
        const string JobStr = "myjob1";
        const string GroupStr = "group1";
        public QuartzNetTest(ITestOutputHelper ouput)
        {
            _ouput = ouput;
            ISchedulerFactory factory = new StdSchedulerFactory();
            _scheduler = factory.GetScheduler().Result;
        }
        /// <summary>
        /// 开始一个任务
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Quartz_Job_Test()
        {
            //_ouput.WriteLine("Test Job");
            //IJobDetail job = JobBuilder.Create<ExampleJob>()
            //    .WithDescription("first job")
            //    .Build();
            //ITrigger trigger = TriggerBuilder.Create()
            //    .StartAt(DateTimeOffset.Now)
            //    .WithSimpleSchedule(a =>
            //    {
            //        a.WithIntervalInSeconds(3);
            //        a.RepeatForever();
            //    }).Build();
            //var date = await _scheduler.ScheduleJob(job, trigger);

            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = await schedulerFactory.GetScheduler();
            await scheduler.Start();
            Console.WriteLine($"任务调度器已启动");

            //创建作业和触发器
            var jobDetail = JobBuilder.Create<ExampleJob>().Build();
            var trigger = TriggerBuilder.Create()
                                        .WithSimpleSchedule(m =>
                                        {
                                            m.WithRepeatCount(3).WithIntervalInSeconds(1);
                                        })
                                        .Build();


            //ObjectUtils.SetPropertyValue();
            //添加调度
            await scheduler.ScheduleJob(jobDetail, trigger);
            Assert.True(true);
        }
        /// <summary>
        /// 暂停一个job
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Quartz_PauseTrigger_Test()
        {
            _ouput.WriteLine("Stop Task");
            //暂停使用
            await _scheduler.PauseTrigger(TriggerKey);
        }
        /// <summary>
        /// 启动一个job
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Quartz_ResumeTrigger_Test()
        {
            _ouput.WriteLine("Start Task");
            //启动使用
            await _scheduler.ResumeTrigger(TriggerKey);
        }

        public TriggerKey TriggerKey
        {
            get
            {
                TriggerKey triggerKey = new TriggerKey(TriggerStr, GroupStr);
                if (triggerKey == null)
                    throw new ArgumentNullException("triggerKey 不能为空！");
                return triggerKey;
            }
        }

        class ExampleJob : IJob
        {
            //readonly ITestOutputHelper _output;
            //public ExampleJob(ITestOutputHelper output)
            //{
            //    _output = output;
            //}
            public async Task Execute(IJobExecutionContext context)
            {
                //throw new ArgumentNullException("xxxx");
                await Console.Out.WriteLineAsync("this a job");
            }
        }
        public class HelloQuartzJob : IJob
        {
            
            public Task Execute(IJobExecutionContext context)
            {
                return Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Hello Quartz.Net");
                });
            }
        }
    }
}
