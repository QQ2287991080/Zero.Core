using DingTalk.Api;
using DingTalk.Api.Request;
using DingTalk.Api.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taobao.Top.Link.Endpoints;
using Zero.Core.Common.DingTalk.Request;
using Zero.Core.Common.Helper;

namespace Zero.Core.Common.DingTalk
{
    public class DingTalkHandlder : IDingTalkHandler
    {
        public DingTalkHandlder()
        {

        }
        public long AgentId
        {
            get
            {
                var agentId = AppsettingHelper.Get("DingTalk:AgentId") ?? throw new Exception("no dingtalk agendId in appseting。");
                return Convert.ToInt64(agentId);
            }
        }

        public string AppKey
        {
            get
            {
                var appKey = AppsettingHelper.Get("DingTalk:AppKey") ?? throw new Exception("no dingtalk appKey in appseting。");
                return appKey;
            }
        }

        public string AppSecret {
            get
            {
                var appSecret = AppsettingHelper.Get("DingTalk:AppSecret") ?? throw new Exception("no dingtalk appSecret in appseting。");
                return appSecret;
            }
        }

        public string GetToken()
        {
            IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/gettoken");
            OapiGettokenRequest request = new OapiGettokenRequest();
            request.Appkey = this.AppKey;
            request.Appsecret = this.AppSecret;
            request.SetHttpMethod("GET");//默认post请求,get请求需要主动指定
            OapiGettokenResponse response = client.Execute(request);
            Console.WriteLine(response.Body);
            return response.AccessToken;
        }

        public DingMessage DingMessage { get;}

    }
}
