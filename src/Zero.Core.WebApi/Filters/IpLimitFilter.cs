using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Common.Helper;
using Zero.Core.Common.Redis;
using Zero.Core.Common.Result;

namespace Zero.Core.WebApi.Filters
{
    /// <summary>
    /// 限制ip访问次数
    /// </summary>
    public class IpLimitFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            int time = AppsettingHelper.Get<int>("IpLimit", "WihtinTime");
            int count = AppsettingHelper.Get<int>("IpLimit", "LimitCount");
            //获取此次的ip请求
            string ipAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();
            if (!string.IsNullOrEmpty(ipAddress))
            {
                var value =  RedisHelper.StringGet<int>(ipAddress);
                if (value == 0)
                {
                     RedisHelper.StringSet(ipAddress, 1, TimeSpan.FromSeconds(time));
                }
                else
                {
                    value++;
                    RedisHelper.StringSet(ipAddress, value, TimeSpan.FromSeconds(time));
                    if (value >= count)
                    {
                        context.Result = AjaxHelper.Seed(Ajax.Bad, "ip limit (every ip has 10 limit)");
                    }
                }
            }
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //获取此次的ip请求
            string ipAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();
            if (!string.IsNullOrEmpty(ipAddress))
            {
                var value = await RedisHelper.StringGetAsync<int>(ipAddress);
                if (value == 0)
                {
                    await RedisHelper.StringSetAsync(ipAddress, 1, TimeSpan.FromSeconds(10));
                }
                else
                {
                    value++;
                    await RedisHelper.StringSetAsync(ipAddress, value, TimeSpan.FromSeconds(10));
                    if (value >= 10)
                    {
                        context.Result = AjaxHelper.Seed(Ajax.Bad, "ip limit (every ip has 10 limit)");
                    }
                }
            }
        }
    }
}
