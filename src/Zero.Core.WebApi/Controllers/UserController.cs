
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Zero.Core.Common.Result;
using Zero.Core.Common.Units;
using Zero.Core.Common.User;
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
        readonly IUserProvider _userProvider;
        readonly IJwtProvider _jwt;
        public UserController(
            IUserService userService,
            IUserProvider userProvider,
            IJwtProvider jwt
            )
        {
            _useService = userService;
            _userProvider = userProvider;
            _jwt = jwt;
        }


        #region Account
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("Login"),AllowAnonymous]
        public async Task<JsonResult> Login(LoginDto dto )
        {

            var token = _jwt.GetJwtToken(new JwtInput() { UserName = dto.UserName });
            return AjaxHelper.Seed(System.Net.HttpStatusCode.OK, "", new { token });
        }
        /// <summary>
        /// 用户登出
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet("LoginOut"),Authorize]
        public async Task<JsonResult> LoginOut(string userName)
        {
            return AjaxHelper.Seed(System.Net.HttpStatusCode.OK, "");
        }
        
        #endregion
    }
}
