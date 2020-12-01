using DingTalk.Api;
using DingTalk.Api.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Common.DingTalk.Config;

namespace Zero.Core.Common.DingTalk.Request
{
    public class DingMessage:DingAppConfig
    {
        public class Message
        {
            public string UserList { get; set; }

            public bool IsQueue { get; set; }
        }
        public DingMessage()
        {

        }
        private Queue<IDingTalkClient> _queue;
        private string _userList;
        private bool _isQueue;
        private DingMessage _message;
        public DingMessage Create(Action<Message> action)
        {
            if (_message == null)
                _message = new DingMessage();

            Message msg = new Message();
            action(msg);
            this._isQueue = msg.IsQueue;
            this._userList = msg.UserList;
            return this;

            //Message message = new Message();
            //action(message);
            //this.isQueue = message.IsQueue;
            //string userList = message.UserList;
            ////实例化dingtalk对象
            //_client = new DefaultDingTalkClient("https://oapi.dingtalk.com/topapi/message/corpconversation/asyncsend_v2");
            ////实例化请求
            //var request = new OapiMessageCorpconversationAsyncsendV2Request();
            ////接受消息人员
            //request.UseridList = userList;
            ////微应用凭证
            //request.AgentId = 1020597343;
            ////是否发送给企业所有人
            //request.ToAllUser = false;
            ////消息内容
            ////_msg = new OapiMessageCorpconversationAsyncsendV2Request.MsgDomain();
            //return this;
        }
        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="content"></param>
        public DingMessage AddText(string content)
        {
            OapiMessageCorpconversationAsyncsendV2Request.MsgDomain msg = new OapiMessageCorpconversationAsyncsendV2Request.MsgDomain();
            msg.Msgtype = "text";
            //消息体
            msg.Text = new OapiMessageCorpconversationAsyncsendV2Request.TextDomain()
            {
                Content = content
            };
            AddMsg(msg);
            return this;
        }
        public void AddImage(string mediaId)
        {
            OapiMessageCorpconversationAsyncsendV2Request.MsgDomain msg = new OapiMessageCorpconversationAsyncsendV2Request.MsgDomain();
            msg.Msgtype = "image";
            //消息体
            msg.Image = new OapiMessageCorpconversationAsyncsendV2Request.ImageDomain()
            {
                MediaId = mediaId
            };
        }
        public void AddVoice(string mediaId, string duration)
        {
            OapiMessageCorpconversationAsyncsendV2Request.MsgDomain msg = new OapiMessageCorpconversationAsyncsendV2Request.MsgDomain();
            msg.Msgtype = "voice";
            msg.Voice = new OapiMessageCorpconversationAsyncsendV2Request.VoiceDomain
            {
                Duration = duration,
                MediaId = mediaId
            };
        }
        public void AddFile(string mediaId)
        {
            OapiMessageCorpconversationAsyncsendV2Request.MsgDomain msg = new OapiMessageCorpconversationAsyncsendV2Request.MsgDomain();
            msg.Msgtype = "file";
            msg.File = new OapiMessageCorpconversationAsyncsendV2Request.FileDomain()
            {
                MediaId = mediaId
            };
          
        }
        public void AddLink(string messageUrl, string picUrl, string title, string text)
        {
            OapiMessageCorpconversationAsyncsendV2Request.MsgDomain msg = new OapiMessageCorpconversationAsyncsendV2Request.MsgDomain();
            msg.Msgtype = "link";
            msg.Link = new OapiMessageCorpconversationAsyncsendV2Request.LinkDomain
            {
                MessageUrl = messageUrl,
                PicUrl = picUrl,
                Text = text,
                Title = title
            };
         
        }
        /// <summary>
        /// 发送OA消息
        /// </summary>
        /// <param name="messageUrl"></param>
        /// <param name="form"></param>
        /// <param name="pcMessageUrl"><see cref=" https://ding-doc.dingtalk.com/doc#/serverapi2/iat9q8"/> </param>
        /// <returns></returns>
        public void AddOA(Dictionary<string, string> form, string messageUrl = "", string pcMessageUrl = "")
        {
            OapiMessageCorpconversationAsyncsendV2Request.MsgDomain msg = new OapiMessageCorpconversationAsyncsendV2Request.MsgDomain();
            msg.Msgtype = "oa";
            //form
            var domains = new List<OapiMessageCorpconversationAsyncsendV2Request.FormDomain>();
            foreach (var item in form)
            {
                domains.Add(new OapiMessageCorpconversationAsyncsendV2Request.FormDomain() { Key = item.Key, Value = item.Value });
            }
            msg.Oa = new OapiMessageCorpconversationAsyncsendV2Request.OADomain
            {
                MessageUrl = messageUrl,
                PcMessageUrl = pcMessageUrl,
                Head = new OapiMessageCorpconversationAsyncsendV2Request.HeadDomain(),
                Body = new OapiMessageCorpconversationAsyncsendV2Request.BodyDomain { Form = domains }
            };
           
        }

        private List<OapiMessageCorpconversationAsyncsendV2Request> _list;

        public void SendMsg()
        {
            ////实例化dingtalk对象
            var client = new DefaultDingTalkClient("https://oapi.dingtalk.com/topapi/message/corpconversation/asyncsend_v2");
            foreach (var item in _list)
            {
                var rsp = client.Execute(item, base.AccessToken);
                Console.WriteLine(rsp.Body);
            }
        }
        private void AddMsg(OapiMessageCorpconversationAsyncsendV2Request.MsgDomain msg)
        {
            ////实例化dingtalk对象
           // var client = new DefaultDingTalkClient("https://oapi.dingtalk.com/topapi/message/corpconversation/asyncsend_v2");
            ////实例化请求
            var request = new OapiMessageCorpconversationAsyncsendV2Request();
            ////接受消息人员
            request.UseridList = _userList;
            ////微应用凭证
            request.AgentId = base.AgentId;
            ////是否发送给企业所有人
            request.ToAllUser = false;
            //消息内容
            request.Msg_ = msg;
            if (_list == null)
                _list = new List<OapiMessageCorpconversationAsyncsendV2Request>();
            _list.Add(request);
        }
    }
}
