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
    /// <summary>
    /// 钉钉消息发送类
    /// </summary>
    public class DingMessage:DingAppConfig
    {
        public class Message
        {
            /// <summary>
            /// 接受消息的钉钉用户
            /// </summary>
            public string UserList { get; set; }
            /// <summary>
            /// 是否可同时发送多条消息
            /// </summary>
            public bool IsQueue { get; set; }
        }

        private const string API = "https://oapi.dingtalk.com/topapi/message/corpconversation/asyncsend_v2";
        private string _userList;
        private bool _isQueue;
        private DingMessage _message;
        private List<OapiMessageCorpconversationAsyncsendV2Request> _list;
        /// <summary>
        /// 创建钉钉发送消息实例
        /// <see cref="DingMessage"/>
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public  DingMessage Create(Action<Message> action)
        {
            if (_message == null)
                _message = new DingMessage();
            
            Message msg = new Message();
            action(msg);
            this._isQueue = msg.IsQueue;
            this._userList = msg.UserList;
            //消息体
            if (_list == null)
                _list = new List<OapiMessageCorpconversationAsyncsendV2Request>();
            return this;
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
        /// <summary>
        /// 发送图片
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public DingMessage AddImage(string mediaId)
        {
            OapiMessageCorpconversationAsyncsendV2Request.MsgDomain msg = new OapiMessageCorpconversationAsyncsendV2Request.MsgDomain();
            msg.Msgtype = "image";
            //消息体
            msg.Image = new OapiMessageCorpconversationAsyncsendV2Request.ImageDomain()
            {
                MediaId = mediaId
            };
            AddMsg(msg);
            return this;
        }
        /// <summary>
        /// 发送语音
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public DingMessage AddVoice(string mediaId, string duration)
        {
            OapiMessageCorpconversationAsyncsendV2Request.MsgDomain msg = new OapiMessageCorpconversationAsyncsendV2Request.MsgDomain();
            msg.Msgtype = "voice";
            msg.Voice = new OapiMessageCorpconversationAsyncsendV2Request.VoiceDomain
            {
                Duration = duration,
                MediaId = mediaId
            };
            AddMsg(msg);
            return this;
        }
        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public DingMessage AddFile(string mediaId)
        {
            OapiMessageCorpconversationAsyncsendV2Request.MsgDomain msg = new OapiMessageCorpconversationAsyncsendV2Request.MsgDomain();
            msg.Msgtype = "file";
            msg.File = new OapiMessageCorpconversationAsyncsendV2Request.FileDomain()
            {
                MediaId = mediaId
            };
            AddMsg(msg);
            return this;
        }
        /// <summary>
        /// 发送链接消息
        /// </summary>
        /// <param name="messageUrl"></param>
        /// <param name="picUrl"></param>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public DingMessage AddLink(string messageUrl, string picUrl, string title, string text)
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
            AddMsg(msg);
            return this;
        }
        /// <summary>
        /// 发送OA消息
        /// </summary>
        /// <param name="messageUrl"></param>
        /// <param name="form"></param>
        /// <param name="pcMessageUrl"><see cref=" https://ding-doc.dingtalk.com/doc#/serverapi2/iat9q8"/> </param>
        /// <returns></returns>
        public DingMessage AddOA(Dictionary<string, string> form, string messageUrl = "", string pcMessageUrl = "")
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
            AddMsg(msg);
            return this;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        public void SendMsg()
        {
            /*
             *发送信息，需要注意的是它可以共用一个client，
             *但是在一定的时间段内是不能发送同样的信息内容的
             */
            var bodies = new List<string>();
            if (_list != null && _list.Count > 0)
            {
                string token = base.AccessToken;
                //实例化dingtalk对象
                var client = new DefaultDingTalkClient(API);
                if (!_isQueue)
                {
                    var rsp = client.Execute(_list.First(), token);
                    bodies.Add(rsp.Body);
                }
                else
                {
                    foreach (var item in _list)
                    {
                        var rsp = client.Execute(item, token);
                        bodies.Add(rsp.Body);
                    }
                }
            }
            this._body = bodies;
        }
        private List<string> _body;
        /// <summary>
        /// 返回消息的Body
        /// </summary>
        public List<string> Body => _body;
        /// <summary>
        /// 消息数量
        /// </summary>
        public int Count => _list.Count;
        private void AddMsg(OapiMessageCorpconversationAsyncsendV2Request.MsgDomain msg)
        {
            if (!_isQueue)
            {
                if (_list != null && _list.Count > 0)
                {
                    return;
                }
            }
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
            _list.Add(request);
        }
    }
}
