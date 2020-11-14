using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Common.Result;
using Zero.Core.WebApi.Hubs;

namespace Zero.Core.WebApi.Filters
{
    /// <summary>
    /// 全局异常处理器
    /// </summary>
    public class SysExceptionFilter : IAsyncExceptionFilter
    {
        private readonly static ILog _log = LogManager.GetLogger(Startup.Logger.Name, typeof(SysExceptionFilter));

        readonly IHubContext<ChatHub> _hub;
        public SysExceptionFilter(IHubContext<ChatHub> hub)
        {
            _hub = hub;
        }
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var error = context.Exception;//错误
            var message = error.Message;//错误提示
            var innerException = error.InnerException?.Message;//内部错误
            var httpContext = context.HttpContext;//此次请求上下文。
            var url = httpContext?.Request.Path.ToUriComponent();//错误发生地址
            //string errMsg= "Url:" + url + ";Message:" + message + "\r\n InnerException:" + innerException;
            
            //写入日志文件描述  注意这个地方尽量不要用中文冒号，否则读取日志文件的时候会造成信息缺失
            string logMessage = $"错误信息=>【{message}】，【请求路由=>{url}】";
            //用户
            log4net.LogicalThreadContext.Properties["UserName"] = httpContext.User.Identity.Name ?? "无";
            //await Task.Run(() => _log.Error("Url:" + url + ";Message:" + message + "\r\n InnerException:" + innerException, error));
            await Task.Run(() => _log.Error(logMessage));
            await _hub.Clients.All.SendAsync("ReceiveLog");
            //返回json内容，如果不这样的话，会返回所有的错误信息。
            context.Result = AjaxHelper.Seed(Ajax.Bad, message);
        }
    }
}
