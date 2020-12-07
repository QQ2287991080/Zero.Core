using DingTalk.Api;
using DingTalk.Api.Request;
using DingTalk.Api.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taobao.Top.Link.Endpoints;
using Zero.Core.Common.DingTalk.Config;
using Zero.Core.Common.DingTalk.Request;
using Zero.Core.Common.Helper;

namespace Zero.Core.Common.DingTalk
{
    public class DingTalkHandlder :DingAppConfig, IDingTalkHandler
    {
        public DingTalkHandlder()
        {
        }

        /// <summary>
        /// 钉钉消息
        /// </summary>
        public DingMessage DingMessage => new DingMessage();
        /// <summary>
        /// 部门管理
        /// </summary>
        public DingDepartment Department => new DingDepartment();
        /// <summary>
        /// 用户管理
        /// </summary>
        public DingUserManager User => new DingUserManager();

    }
}
