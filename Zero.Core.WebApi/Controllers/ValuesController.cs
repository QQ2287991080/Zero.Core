using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zero.Core.Common.Result;
using Zero.Core.Common.User;
using Zero.Core.Domain.Entities;
using Zero.Core.IServices;
using Zero.Core.WebApi.ServiceExtensions;

namespace Zero.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(CorsExtension.PolicyName)]
    public class ValuesController : ControllerBase
    {
        readonly IUserService _user;
        readonly IUserProvider _provider;
        public ValuesController(
            IUserService user,
            IUserProvider provider
            ) 
        {
            _user = user;
            _provider = provider;
        }
        [HttpGet("TestUserProvider")]
        public JsonResult TestUserProvider()
        {
            //string userName = _provider.UserName();
            _user.UserName();
            return ResultHelper.Seed(System.Net.HttpStatusCode.OK, "成功");
        }

        [HttpGet]
        [Route("Data")]
        public async Task<JsonResult> Data()
        {
            var list = await _user.GetAllAsync();
            return ResultHelper.Seed(System.Net.HttpStatusCode.OK, "Fine", list);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<JsonResult> Add()
        {
            var entity = await _user.AddAsync(new User()
            {

                RealName = "飞天猪皮怪",
                UserName = "Zero",
                Password = "111111",
                Email = "2287991080@qq.com",
                Phone = "15268983151",
                Sex = 1,
                Remark = "ppgod",
                Salt = "xxxx",
            });
            return ResultHelper.Seed(System.Net.HttpStatusCode.OK, "操作成功", entity);
        }


      
    }
}
