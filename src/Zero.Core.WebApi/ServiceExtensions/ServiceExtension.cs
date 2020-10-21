﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Common.Helper;
using Zero.Core.Common.Units;
using Zero.Core.Common.User;

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
            #endregion
            return services;
        }
    }
}