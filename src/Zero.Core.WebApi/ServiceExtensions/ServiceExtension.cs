using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Common.Helper;
using Zero.Core.Common.User;
using Zero.Core.Common.Utils;

namespace Zero.Core.WebApi.ServiceExtensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddService(this IServiceCollection  services)
        {
            //appsetting读取类
            services.AddSingleton(new AppsettingHelper());

            #region Common Module
            //用户核心类
            services.AddTransient<IUserProvider, UserProvider>();
            //jwt help
            services.AddTransient<IJwtProvider, JwtHelper>();
            //log
            services.AddSingleton<ILogHelper, LogHelper>();
            services.AddSingleton(new LogSignalRHelper());
            services.AddSingleton(typeof(LoggerHelper));
            #endregion
            return services;
        }
    }
}
