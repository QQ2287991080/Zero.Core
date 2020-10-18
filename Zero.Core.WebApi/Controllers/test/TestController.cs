using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
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
    }
}
