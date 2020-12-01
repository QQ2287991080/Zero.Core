using DingTalk.Api;
using DingTalk.Api.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.Common.DingTalk.Request
{
    public class DingMessage
    {
        public class Message
        {
            public string UserList { get; set; }

            public bool IsQueue { get; set; }
        }
        public DingMessage()
        {

        }
        string _userList;
        OapiMessageCorpconversationAsyncsendV2Request.MsgDomain _msg;
        OapiMessageCorpconversationAsyncsendV2Request _request;
        IDingTalkClient _client;
        //private  Queue<IDingTalkClient> 
        private bool isQueue;
        public DingMessage Create(Action<Message> action)
        {
            Message message = new Message();
            action(message);
            this.isQueue = message.IsQueue;
            string userList = message.UserList;
            //实例化dingtalk对象
            _client = new DefaultDingTalkClient("https://oapi.dingtalk.com/topapi/message/corpconversation/asyncsend_v2");
            //实例化请求
            var request = new OapiMessageCorpconversationAsyncsendV2Request();
            //接受消息人员
            request.UseridList = userList;
            //微应用凭证
            request.AgentId = 1020597343;
            //是否发送给企业所有人
            request.ToAllUser = false;
            //消息内容
            //_msg = new OapiMessageCorpconversationAsyncsendV2Request.MsgDomain();
            return this;
        }
        public void AddText(string content)
        {
            _msg.Msgtype = "text";
            //消息体
            _msg.Text = new OapiMessageCorpconversationAsyncsendV2Request.TextDomain()
            {
                Content = content
            };
            _request.Msg_ = _msg;
        }
        public void AddImage(string mediaId)
        {
            _msg.Msgtype = "image";
            //消息体
            _msg.Image = new OapiMessageCorpconversationAsyncsendV2Request.ImageDomain()
            {
                MediaId = mediaId
            };
            _request.Msg_ = _msg;
        }
        public void AddVoice(string mediaId, string duration)
        {
            _msg.Msgtype = "voice";
            _msg.Voice = new OapiMessageCorpconversationAsyncsendV2Request.VoiceDomain
            {
                Duration = duration,
                MediaId = mediaId
            };
            _request.Msg_ = _msg;
        }
        public void AddFile(string mediaId)
        {
            _msg.Msgtype = "file";
            _msg.File = new OapiMessageCorpconversationAsyncsendV2Request.FileDomain()
            {
                MediaId = mediaId
            };
            _request.Msg_ = _msg;
        }
        public void AddLink(string messageUrl, string picUrl, string title, string text)
        {
            _msg.Msgtype = "link";
            _msg.Link = new OapiMessageCorpconversationAsyncsendV2Request.LinkDomain
            {
                MessageUrl = messageUrl,
                PicUrl = picUrl,
                Text = text,
                Title = title
            };
            _request.Msg_ = _msg;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageUrl"></param>
        /// <param name="form"></param>
        /// <param name="pcMessageUrl"><see cref=" https://ding-doc.dingtalk.com/doc#/serverapi2/iat9q8"/> </param>
        /// <returns></returns>
        public void AddOA(Dictionary<string, string> form, string messageUrl = "", string pcMessageUrl = "")
        {
            _msg.Msgtype = "oa";
            //form
            var domains = new List<OapiMessageCorpconversationAsyncsendV2Request.FormDomain>();
            foreach (var item in form)
            {
                domains.Add(new OapiMessageCorpconversationAsyncsendV2Request.FormDomain() { Key = item.Key, Value = item.Value });
            }
            _msg.Oa = new OapiMessageCorpconversationAsyncsendV2Request.OADomain
            {
                MessageUrl = messageUrl,
                PcMessageUrl = pcMessageUrl,
                Head = new OapiMessageCorpconversationAsyncsendV2Request.HeadDomain(),
                Body = new OapiMessageCorpconversationAsyncsendV2Request.BodyDomain { Form = domains }
            };
            _request.Msg_ = _msg;
        }
    }
}
