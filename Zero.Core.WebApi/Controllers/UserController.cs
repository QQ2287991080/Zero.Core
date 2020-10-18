
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Zero.Core.Common.Result;
using Zero.Core.Domain.Dtos.User;
using Zero.Core.IServices;

namespace Zero.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("用户相关接口")]
    public class UserController : ControllerBase
    {
        readonly IUserService _useService;
        public UserController(
            IUserService userService
            )
        {
            _useService = userService;
        }


        #region Account
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<JsonResult> Login(LoginDto dto )
        {

            return AjaxHelper.Seed(System.Net.HttpStatusCode.OK, "");
        }
        /// <summary>
        /// 用户登出
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet("LoginOut")]
        public async Task<JsonResult> LoginOut(string userName)
        {
            return AjaxHelper.Seed(System.Net.HttpStatusCode.OK, "");
        }
        
        #endregion
    }
}
