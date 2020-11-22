using Microsoft.Extensions.DependencyInjection;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Quartz.Base;
using Zero.Core.Quartz.Factory;
using Zero.Core.Quartz.Jobs;

namespace Zero.Core.WebApi.ServiceExtensions
{
    /// <summary>
    /// Quartz 任务调度
    /// </summary>
    public static class JobExtension
    {
        public static IServiceCollection AddQuartz(this IServiceCollection services)
        {

            //自定义Job工厂
            services.AddSingleton<IJobFactory, ZeroJobFactory>();
            //任务调度控制中心
            services.AddSingleton<IControllerCenter, ControllerCenter>();
            //Jobs,将组件好的Job放在这里，生命周期为瞬时的
            services.AddTransient<FirstJob>();
            return services;
        }
    }
}
