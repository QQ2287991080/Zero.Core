using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Swashbuckle.AspNetCore.Annotations;
using Zero.Core.Common.Helper;
using Zero.Core.Common.Result;
using Zero.Core.WebApi.Hubs;

namespace Zero.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [SwaggerTag("SignalR")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        readonly IHubContext<ChatHub> _hub;
        public ChatController(IHubContext<ChatHub> hub)
        {
            _hub = hub;
        }

        /// <summary>
        /// 被动向客户端发送日志信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("SendToClient")]
        public async Task<JsonResult> SendToClient(DateTime? date)
        {
            var data = LogSignalRHelper.Read(date);
            await _hub.Clients.All.SendAsync("ReceiveLog", data);
            return AjaxHelper.Seed(Ajax.Ok);
        }

        /// <summary>
        /// 获取日志信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDataList")]
        public async Task<JsonResult> GetDataList()
        {
            var data = await Task.Run(() => LogSignalRHelper.Read());
            return AjaxHelper.Seed(Ajax.Ok, data);
        }
    }
}
