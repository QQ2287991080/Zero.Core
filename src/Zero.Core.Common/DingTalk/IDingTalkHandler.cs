using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Common.DingTalk.Config;
using Zero.Core.Common.DingTalk.Request;

namespace Zero.Core.Common.DingTalk
{
    public interface IDingTalkHandler : IDingAppConfig
    {
        /// <summary>
        /// 钉钉消息通知
        /// </summary>
        DingMessage DingMessage { get; }
        /// <summary>
        /// 部门管理
        /// </summary>
        DingDepartment Department { get; }
        /// <summary>
        /// 用户管理
        /// </summary>
        DingUserManager User { get; }
    }
}
