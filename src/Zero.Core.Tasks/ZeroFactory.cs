using Quartz;
using Quartz.Spi;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;
using Quartz.Simpl;

namespace Zero.Core.Tasks
{
    /// <summary>
    /// 自定义任务工厂
    /// 
    /// https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/miscellaneous-features.html#plug-ins
    /// </summary>
    public class ZeroFactory: IJobFactory
    {
        readonly IServiceProvider _provider;
        public ZeroFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public  IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                //var service = _provider.CreateScope();
                //var job= service.ServiceProvider.GetService(bundle.JobDetail.JobType) as IJob;
                var job= _provider.GetService(bundle.JobDetail.JobType) as IJob;
                return job;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public  void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }
}
