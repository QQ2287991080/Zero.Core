using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.WebApi.Hubs
{
    /// <summary>
    /// https://docs.microsoft.com/zh-cn/aspnet/core/signalr/hubs?view=aspnetcore-3.1
    /// 强类型中心
    /// </summary>
    public interface IChatClient
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="message">消息</param>
        /// <returns></returns>
        Task ReceiveMessage(string user, string message);
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        Task ReceiveMessage(object message);
        /// <summary>
        /// 像调用（客户端）发送消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task ReceiveCaller(object message);
        /// <summary>
        /// 发送日志信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task ReceiveLog(object data = null);
    }
}
