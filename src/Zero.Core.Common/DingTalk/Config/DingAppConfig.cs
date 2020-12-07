using DingTalk.Api;
using DingTalk.Api.Request;
using DingTalk.Api.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Common.Helper;

namespace Zero.Core.Common.DingTalk.Config
{
    public abstract class DingAppConfig:IDingAppConfig
    {

        private static long _agentId;
        private static string _appKey;
        private static string _appSecret;
        public long AgentId
        {
            get
            {

                if (_agentId == 0)
                {
                    var agentId = AppsettingHelper.Get("DingTalk:AgentId") ?? throw new Exception("no dingtalk agendId in appseting。");
                    _agentId = Convert.ToInt64(agentId);
                }
                return _agentId;
            }
        }

        public string AppKey
        {
            get
            {
                if (string.IsNullOrEmpty(_appKey))
                {
                    _appKey = AppsettingHelper.Get("DingTalk:AppKey") ?? throw new Exception("no dingtalk appKey in appseting。");
                }
                return _appKey;
            }
        }

        public string AppSecret
        {
            get
            {
                if (string.IsNullOrEmpty(_appSecret))
                    _appSecret = AppsettingHelper.Get("DingTalk:AppSecret") ?? throw new Exception("no dingtalk appSecret in appseting。");
                return _appSecret;
            }
        }

        /// <summary>
        /// token可以自定义放在redis里面，怎么做都可以
        /// 钉钉其实在token的时间上面也是做了限制的。
        /// </summary>
        private string _token;
        public string AccessToken
        {
            get
            {
                if (string.IsNullOrEmpty(_token))
                    _token = GetToken();
                return _token;
            }
            protected set
            {
                _token = value;
            }
        }

        
        public virtual string GetToken()
        {
            //Console.WriteLine($"Appkey：【{this.AppKey}】");
            //Console.WriteLine($"AppSecret：【{ this.AppSecret}】");
            IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/gettoken");
            OapiGettokenRequest request = new OapiGettokenRequest();
            request.Appkey = this.AppKey;
            request.Appsecret = this.AppSecret;
            request.SetHttpMethod("GET");//默认post请求,get请求需要主动指定
            OapiGettokenResponse response = client.Execute(request);
            //Console.WriteLine(response.Body);
            return response.AccessToken;
        }
    }
}
