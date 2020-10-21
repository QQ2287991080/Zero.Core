using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.WebApi.ServiceExtensions
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddZeroAutoMapper(this IServiceCollection services)
        {
            //IConfigurationProvider
            var assembly = Assembly.LoadFile(AppContext.BaseDirectory + "Zero.Core.WebApi.dll");


            var assembly2 = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly2);
            return services;
        }
    }
}
