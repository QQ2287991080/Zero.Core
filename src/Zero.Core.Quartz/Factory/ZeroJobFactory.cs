using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.Quartz.Factory
{
    /// <summary>
    /// 自定义job工厂
    ///  https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/miscellaneous-features.html#plug-ins
    /// </summary>
    public class ZeroJobFactory : IJobFactory
    {
        readonly IServiceProvider _provider;
        public ZeroJobFactory(IServiceProvider provider)
        {
            _provider = provider;
        }
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                //从Quartz.net的源码实现net core注入这一块能够发现，job实例是通过AddTransient加入容器中的
                //还有自定义的JobFactory也需要单例注入，我觉的是因为如果不单例注入会导致Quartz使用默认的SimpleJobFactory
                //从而导致这里的获取Job实例出问题。
                var service = _provider.CreateScope();
                var job = service.ServiceProvider.GetService(bundle.JobDetail.JobType) as IJob;
                return job;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 允许工厂释放Job
        /// </summary>
        /// <param name="job"></param>
        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }
}
