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
                //a.DefaultModelExpandDepth(0);
                //a.DefaultModelRendering(ModelRendering.Example);//返回类型，优先展示方式，默认展示Example
                a.DefaultModelsExpandDepth(-1);//用于控制最下面展示的模型，-1是全部隐藏
                //a.DisplayOperationId();//控制操作列表中操作ID的显示
                a.DisplayRequestDuration();//控制试用请求的请求持续时间（以毫秒为单位）的显示
                //a.DocExpansion(DocExpansion.List);//list 展开控制器下的所有接口，Full打开是所有接口的调试界面，None全部收缩,不配置的情况下是list
                a.EnableDeepLinking();//为标记和操作启用深度链接
                a.EnableFilter();//expression参数，在输入输入框中默认显示
                a.ShowExtensions();
                // Network
                //a.EnableValidator();//您可以使用此参数启用swagger ui的内置验证器（badge）功能，将其设置为null将禁用验证
                //a.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Post);//启用的请求方式,如果不启用那么将无法调试

                a.DocumentTitle = "Zero.Core";
                a.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                a.ShowExtensions();
            });
        }
    }
}
