using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.Common.DingTalk
{
    public interface IDingTalkHandler
    {
        #region 微应用凭证
        /// <summary>
        /// 微应用 AgentId
        /// </summary>
        long AgentId { get;}
        /// <summary>
        /// 微应用唯一标识
        /// </summary>
        string AppKey { get; }
        /// <summary>
        /// 微应用的密钥
        /// </summary>
        string AppSecret { get; }
        #endregion
        /// <summary>
        /// 获取钉钉token
        /// </summary>
        /// <returns></returns>
        string GetToken();
    }
}
