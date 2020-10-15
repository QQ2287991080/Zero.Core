using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.WebApi.StartupExtensions
{
    public static class SwaggerExtension
    {
        public static void UseSwaggerDocs(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(a =>
            {
                a.DocumentTitle = "Zero.Core";
                a.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                a.ShowExtensions();
            });
        }
    }
}
