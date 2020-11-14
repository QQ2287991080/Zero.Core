using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Common.Helper;
using Zero.Core.Common.Result;
using Zero.Core.WebApi.Hubs;

namespace Zero.Core.WebApi.Middlewares
{
    public class ExceptionMiddleware
    {
        public async Task Invoke(HttpContext context)
        {
            
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null && contextFeature.Error != null)
            {
                context.Response.StatusCode = 200;
                context.Response.ContentType = "application/json";


                string message = $"错误信息=>【{contextFeature?.Error?.Message ?? "无"}】，【中间件错误=>🐕】";

                await context.Response.WriteAsync(JsonConvert.SerializeObject(new AjaxHelper.Result
                {
                    ErrCode = HttpStatusCode.InternalServerError,
                    ErrMsg = message
                }));
            }
        }
    }
}
