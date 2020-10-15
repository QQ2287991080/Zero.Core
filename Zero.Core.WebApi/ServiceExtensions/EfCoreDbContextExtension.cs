using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Common.Helper;
using Zero.Core.EfCore;

namespace Zero.Core.WebApi.ServiceConfig
{
    public static class EfCoreDbContextExtension
    {
        public static IServiceCollection AddEfDbContext(this IServiceCollection services)
        {
            var conStr = AppsettingHelper.Get("DataConnection", "SqlServer");
            services.AddDbContext<EfCoreDbContext>(option =>
            {
                option.UseSqlServer(conStr);
            });

            //services.AddScoped<EfCoreDbContext>();
            return services;
        }
    }
}
