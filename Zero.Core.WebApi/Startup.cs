using Autofac;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;
using Zero.Core.WebApi.Filters;
using Zero.Core.WebApi.Middlewares;
using Zero.Core.WebApi.ServiceConfig;
using Zero.Core.WebApi.ServiceExtensions;
using Zero.Core.WebApi.StartupExtensions;
#if DEBUG
//[assembly:ApiController]
#else
[assembly:ApiController]
#endif
namespace Zero.Core.WebApi
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
              .AddEnvironmentVariables();
            Configuration = builder.Build();
            Logger = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            XmlConfigurator.Configure(Logger, new FileInfo("log4net.config"));
        }
        public static ILoggerRepository Logger { get; set; }
        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; private set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //程序依赖注入
            services.AddService();
            #region Framework
            //控制器配置
            services.AddControllers()
            #region Newtonsoft Configure
            .AddNewtonsoftJson(
               option =>
               {
                   option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;//忽略循环引用
                   option.SerializerSettings.NullValueHandling = NullValueHandling.Include;//是否忽略空值引用
                   option.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();//驼峰命名
                   option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";//时间格式化
               });
            #endregion
            /*
             *httpcontext 引用
             *https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/http-context?view=aspnetcore-3.1
             *IHttpContextAccessor
             */
            services.AddHttpContextAccessor();
            /*
             *为控制器添加拦截器或验证等
             */
            services.AddMvcCore(options =>
            {
                //全局异常过滤器
                options.Filters.Add(typeof(SysExceptionFilter));
                //限制响应数据格式
                options.Filters.Add(new ProducesAttribute("application/json"));
                //全局添加授权认证
                //var policy = new AuthorizationPolicyBuilder()
                //.RequireAuthenticatedUser()
                //.Build();
                //options.Filters.Add(new AuthorizeFilter(policy));
            });
            #endregion

            #region Extension

            //ef 
            services.AddEfDbContext();
            //swagger
            services.AddSwaggerDocs();
            //jwt
            services.AddJwtToken();
            //跨域
            services.AddZeroCors();
            //automapper
            services.AddZeroAutoMapper();
            #endregion
        }
        /// <summary>
        /// autofac 接管 ioc
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            //log4  Is't must
            var lf = loggerFactory.AddLog4Net();
            //请求日志
            app.UseRequestLog();

            app.UseRouting();
            //跨域
            app.UseCors(CorsExtension.PolicyName);
            /*
             *启用授权验证
             *例如 使用jwt之后就需要加上启用该中间件
             *否则就无法验证
             */
            app.UseAuthentication();
            /*
             * 启用授权
             * 可以这么理解：如果我们在使用Authorize进行授权验证
             * 如果这个中间件没有启动的话，是会出现错误的，
             * Authorizaton 在中间件管道中的位置在Authentication 之后的
             * 这也是为什么需要注意在Confiure中代码的顺序问题
             */
            app.UseAuthorization();
            /*
             *  需要与useRouing 一起使用
             */
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors(CorsExtension.PolicyName);
                //SignalR
            });
            //swagger in  middleware
            app.UseSwaggerDocs();

        }
    }
}
