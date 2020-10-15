using Autofac;
using log4net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.WebApi.Middlewares
{
    /// <summary>
    /// 记录请求信息
    /// </summary>
    public class RequestLogMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ILog _logger = LogManager.GetLogger(Startup.Logger.Name, typeof(RequestLogMiddleware));

        public RequestLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            var path = httpContext.Request.Path.ToUriComponent();//请求的控制器和方法
            if (!path.Contains("swagger"))
            {
                var host = httpContext.Request.Host.ToUriComponent();//ip+端口
                var query = httpContext.Request.QueryString.ToUriComponent();//请求参数
                _logger.Warn($"【Host】{host}，【路由】{path}，【请求参数】{query}");
            }
            await _next(httpContext);
        }
    }
    public static class RequestLogMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLog(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLogMiddleware>();
        }
    }
}
