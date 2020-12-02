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
            this.DingMessage = new DingMessage();
        }

        public DingMessage DingMessage { get;}

    }
}
