using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public TestController(
            IMapper mapper,
            IUserService user
            )
        {
            _mapper = mapper;
            _user = user;
        }

        /// <summary>
        /// 测试automapper
        /// </summary>
        /// <returns></returns>
        [HttpGet("testMapper")]
        public JsonResult testMapper()
        {
            var user = _user.Query().FirstOrDefault();
            var dto = _mapper.Map<UserDto>(user);
            return ResultHelper.Seed(System.Net.HttpStatusCode.OK, "OK", dto);
        }
    }
}
