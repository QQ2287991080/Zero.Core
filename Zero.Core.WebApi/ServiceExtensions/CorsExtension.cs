using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.WebApi.ServiceExtensions
{
    public static class CorsExtension
    {
        public const string PolicyName = "All";
        public static IServiceCollection AddZeroCors(this IServiceCollection services)
        {
            services.AddCors(option =>
            {
                option.AddPolicy(PolicyName, p => {
                    p.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetPreflightMaxAge(TimeSpan.FromMilliseconds(100000));//设置预检请求(OPTIONS)缓存时间
                });
            });
            return services;
        }
    }

    
}
