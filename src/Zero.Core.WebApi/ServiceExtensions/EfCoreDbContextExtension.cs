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
            string dbType = AppsettingHelper.Get("DataConnection", "DbType");
            var conStr = AppsettingHelper.Get("DataConnection", dbType);
            services.AddDbContext<EfCoreDbContext>(option =>
            {
                if (dbType == "SqlServer")
                {
                    option.UseSqlServer(conStr);
                }
                else
                {
                    option.UseMySQL(conStr);
                }
            });

            //services.AddScoped<EfCoreDbContext>();
            return services;
        }
    }
}
