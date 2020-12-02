using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zero.Core.Common.DingTalk;
using Zero.Core.Common.Result;
using Zero.Core.IServices;
using Zero.Core.WebApi.Mapper;

namespace Zero.Core.WebApi.Controllers.test
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        readonly IMapper _mapper;
        readonly IUserService _user;
        readonly ILogger<TestController> _logger;
        readonly IDingTalkHandler _ding;
        public TestController(
            IMapper mapper,
            IUserService user,
            ILogger<TestController> logger,
            IDingTalkHandler ding
            )
        {
            _mapper = mapper;
            _user = user;
            _logger = logger;
            _ding = ding;
        }

        /// <summary>
        /// 测试钉钉消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="isQueue"></param>
        /// <returns></returns>
        [HttpGet("DingMessage")]
        public JsonResult DingMessage(string msg,bool isQueue=false)
        {
            var keys = new Dictionary<string, string>();
            keys.Add("标题", DateTime.Now.ToString());
            var message = _ding.DingMessage.Create(a => {
                a.UserList = "manager7692";
                a.IsQueue = isQueue;
            });
            message.AddText(msg)
                .AddOA(keys)
                .SendMsg();

            Console.WriteLine($"发送消息个数：{message.Count}");
            foreach (var item in message.Body)
            {
                Console.WriteLine($"消息结果：{item}");
            }
            return AjaxHelper.Seed(Ajax.Ok);
        }
        /// <summary>
        /// 设置一个错误
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpGet("setErr")]
        public JsonResult SetErr(string message)
        {
            throw new Exception(string.IsNullOrEmpty(message) ? "错误测试" : message);
        }

        /// <summary>
        /// 测试automapper
        /// </summary>
        /// <returns></returns>
        [HttpGet("testMapper")]
        public JsonResult testMapper()
        {
            _logger.LogInformation("xxx");
            var user = _user.Query().FirstOrDefault();
            var dto = _mapper.Map<UserDto>(user);
            return AjaxHelper.Seed(System.Net.HttpStatusCode.OK, "OK", dto);
        }
        [HttpGet("TestEFCoreFirst")]
        public async Task<JsonResult> One(string name)
        {
            var user = await _user.FirstAsync(f => f.UserName == name);
            return AjaxHelper.Seed(Ajax.Ok, user);
        }
        [HttpGet("TestEFCoreFirst2")]
        public  JsonResult Tow(string name)
        {
            var user = _user.First(f => f.UserName == name);
            return AjaxHelper.Seed(Ajax.Ok, user);
        }
        [HttpGet("TestEFCoreFirst3")]
        public async Task<JsonResult> Three(int id)
        {
            var user = await _user.FirstAsync(f => f.Id == id);
            return AjaxHelper.Seed(Ajax.Ok, user);
        }
    }
}
