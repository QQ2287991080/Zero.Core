using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Common.Helper;

namespace Zero.Core.WebApi.Hubs
{
    public class ChatHub:Hub<IChatClient>
    {

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.ReceiveMessage(user, message);
        }

        public async Task SendMessageCaller(string message)
        {
            await Clients.Caller.ReceiveCaller(message);
        }

        /// <summary>
        /// 客户端连接事件
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            var id = Context.ConnectionId;
            return base.OnConnectedAsync();
        }

        /// <summary>
        /// 客户端断开事件
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var id = Context.ConnectionId;
            return base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// 向客户端推送日志消息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task RecevieLog(object data=null)
        {
            if (data == null)
                data = LogSignalRHelper.Read();
            await Clients.All.ReceiveLog(data);
        }
    }
}
