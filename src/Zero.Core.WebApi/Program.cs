using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Zero.Core.WebApi
{
    public class Program
    {
        /// <summary>
        /// .net 通用主机
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .ConfigureLogging(log =>
                    {
                        //过滤系统日志内容
                        log.AddFilter("Microsoft", LogLevel.Warning);
                        log.AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information);
                        log.AddFilter("System", LogLevel.Warning);
                        log.AddFilter("LoggingConsoleApp.Program", LogLevel.Warning);

                        log.AddConsole();
                        log.AddDebug();
                        log.SetMinimumLevel(LogLevel.Warning);
                        log.AddLog4Net();
                    });
                });

    }
}
