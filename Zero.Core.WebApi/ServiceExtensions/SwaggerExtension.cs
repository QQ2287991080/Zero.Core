using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Common.Helper;

namespace Zero.Core.WebApi.ServiceExtensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
        {
            services.AddSwaggerGen(i =>
            {

                i.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Zero.Core.WebApi Docs",
                    Description = "WebApi",
                    TermsOfService = new Uri("https://www.baidu.com"),
                    Contact = new OpenApiContact { Name = "张力", Email = "2287991080@qq.com" },//联系我
                    License = new OpenApiLicense { Name = "博客园", Url = new Uri("https://www.cnblogs.com/aqgy12138/") }//许可
                });

                //i.ResolveConflictingActions(o => o.First());//控制器允许同名重载方法
                i.EnableAnnotations();//注释

                // 开启加权小锁
                i.OperationFilter<AddResponseHeadersFilter>();
                i.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                #region Tip2 二选一
                // 在header中添加token，传递到后台
                i.OperationFilter<SecurityRequirementsOperationFilter>();
                #endregion
                // 添加Header验证消息
                i.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token(注意Bearer和Token之间的空格)：Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                #region Tip2 二选一
                //i.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //   {
                //      new OpenApiSecurityScheme
                //      {
                //        Reference = new OpenApiReference {
                //        Type = ReferenceType.SecurityScheme,
                //        Id = "Bearer"
                //       }
                //    },
                //     new string[] { }
                //   }
                //});
                #endregion
                //设置swagger备注
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var xmls = AppsettingHelper.Get<string[]>("SwaggerXml");
                for (int x = 0; x < xmls.Length; x++)
                {
                    var xmlPath = Path.Combine(basePath, xmls[x]);
                    i.IncludeXmlComments(xmlPath);//文档中文提示
                }
            });
            return services;
        }
    }
}
