using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Common.Helper;
using Zero.Core.Common.Redis;
using Zero.Core.Common.Result;

namespace Zero.Core.WebApi.Middlewares
{
    public class IpMiddleware
    {
        readonly RequestDelegate _next;
        public IpMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext httpContext)
        {
            int time = AppsettingHelper.Get<int>("IpLimit", "WihtinTime");
            int count = AppsettingHelper.Get<int>("IpLimit", "LimitCount");
            //获取此次的ip请求
            string ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();
            if (!string.IsNullOrEmpty(ipAddress))
            {
                var value = await RedisHelper.StringGetAsync<int>(ipAddress);
                if (value == 0)
                {
                    await RedisHelper.StringSetAsync(ipAddress, 1, TimeSpan.FromSeconds(time));
                }
                else
                {
                    value++;
                    await RedisHelper.StringSetAsync(ipAddress, value, TimeSpan.FromSeconds(time));
                    if (value >= count)
                    {
                        throw new Exception("ip limit (every ip has 10 limit)");
                    }
                }
            }
            await _next(httpContext);
        }
    }

    public static class IpMiddlewareExtension
    {
        public static IApplicationBuilder UseIpLimit(this IApplicationBuilder app)
        {
            app.UseMiddleware<IpMiddleware>();
            return app;
        }
    }
}
