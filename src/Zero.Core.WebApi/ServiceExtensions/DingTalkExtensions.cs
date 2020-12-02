using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Common.DingTalk;

namespace Zero.Core.WebApi.ServiceExtensions
{
    public static class DingTalkExtensions
    {
        public static IServiceCollection AddDingTalk(this IServiceCollection services)
        {
            services.AddTransient<IDingTalkHandler, DingTalkHandlder>();
            return services;
        }
    }
}
