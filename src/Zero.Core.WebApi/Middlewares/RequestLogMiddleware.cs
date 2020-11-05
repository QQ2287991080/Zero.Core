using Autofac;
using log4net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
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
            //获取客户端ip
            var ipAddress = httpContext.Connection.RemoteIpAddress;
            var request = httpContext.Request;
            var path = request.Path.ToUriComponent();//请求的控制器和方法
            if (!path.Contains("swagger"))
            {
                var host = request.Host.ToUriComponent();//ip+端口
                //var query = request.QueryString.ToUriComponent();//请求参数
                //请求方式
                var method = request.Method;
                string parameter = "";
                switch (method)
                {
                    case "GET":
                        //解码
                        var encode = System.Web.HttpUtility.UrlDecode(request.QueryString.ToUriComponent());
                        parameter = encode;
                        break;
                    case "POST":
                        if (request.ContentType == null) break;
                        //form 表单
                        if (request.ContentType.Contains("application/x-www-form-urlencoded"))
                        {
                            parameter = request.Form.FormToString();
                        }
                        //form body
                        if (request.ContentType.Contains("application/json"))
                        {
                            request.EnableBuffering();
                            var body = request.Body;
                            body.Seek(0, SeekOrigin.Begin);
                            //读取body内容
                            var read = new StreamReader(body, Encoding.UTF8);
                            parameter = await read.ReadToEndAsync();
                            //这里不能使用 using 或者 read.Close方法因为它会导致 body中的流关闭，从而导致无法绑定参数
                            body.Seek(0, SeekOrigin.Begin);
                        }
                        //文件
                        if (request.ContentType.Contains("form-data"))
                        {
                            //表单中的文件
                            var formFile = request.Form.Files;
                            parameter = request.Form.FormToString() + "\r\n" + formFile.FormFilesToString();
                        }
                        break;
                    default:
                        break;
                }
                _logger.Warn($"【ClientIP】{ipAddress?.ToString()}【Host】{host}，【路由】{path}，【请求参数】{parameter}");
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


    public static class RequestExtesnion
    {
        public static string FormToString(this IFormCollection keys)
        {
            List<string> paramters = new List<string>();
            foreach (var item in keys)
            {
                string paramter = item.Key + ":" + item.Value;
                paramters.Add(paramter);
            }
            return string.Join("\r\n", paramters);
        }
        public static string FormFilesToString(this IFormFileCollection  formFiles)
        {
            List<string> paramters = new List<string>();
            foreach (var item in formFiles)
            {
                string paramter = $"{item.Name}:文件名称【{item.FileName}】,内容长度【{item.Length}】";
                paramters.Add(paramter);
            }
            return string.Join("\r\n", paramters);
        }
    }
}
