using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Common.Result;

namespace Zero.Core.WebApi.Filters
{
    /// <summary>
    /// 全局异常处理器
    /// </summary>
    public class SysExceptionFilter : IAsyncExceptionFilter
    {
        private readonly static ILog _log = LogManager.GetLogger(Startup.Logger.Name, typeof(SysExceptionFilter));
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var error = context.Exception;//错误
            var message = error.Message;//错误提示
            var innerException = error.InnerException?.Message;//内部错误
            var httpContext = context.HttpContext;//此次请求上下文。
            var url = httpContext?.Request.Path.ToUriComponent();//错误发生地址
            string errMsg= "Url:" + url + ";Message:" + message + "\r\n InnerException:" + innerException;
            await Task.Run(() => _log.Error("Url:" + url + ";Message:" + message + "\r\n InnerException:" + innerException, error));
            //返回json内容，如果不这样的话，会返回所有的错误信息。
            context.Result = AjaxHelper.Seed(System.Net.HttpStatusCode.BadRequest, errMsg);
        }
    }
}
