using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
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
        public TestController(
            IMapper mapper,
            IUserService user,
            ILogger<TestController> logger
            )
        {
            _mapper = mapper;
            _user = user;
            _logger = logger;
        }

        [HttpGet("setErr")]
        public JsonResult SetErr()
        {
            throw new Exception("错误测试");
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
