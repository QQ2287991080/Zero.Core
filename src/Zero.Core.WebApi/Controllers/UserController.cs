
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
using Zero.Core.Domain.Entities;
using Zero.Core.IServices;

namespace Zero.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [SwaggerTag("用户相关接口")]
    [Authorize]
    public class UserController : ControllerBase
    {
        readonly IUserService _useService;
        readonly IUserProvider _userProvider;
        public UserController(
            IUserService userService,
            IUserProvider userProvider
            )
        {
            _useService = userService;
            _userProvider = userProvider;
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
            var user = await _useService.FirstAsync(f => dto.UserName == dto.UserName);
            if (user == null)
            {
                return AjaxHelper.Seed(System.Net.HttpStatusCode.BadRequest, "用户名错误");
            }
            if (user.Password != dto.Password)
            {
                return AjaxHelper.Seed(System.Net.HttpStatusCode.BadRequest, "密码错误");
            }
            //创建token
            var token = _userProvider.CreateJwtToken(new JwtInput() { UserName = dto.UserName });
            //保存用户信息
            await _userProvider.SetToken(dto.UserName, token.Token, token.Time);
            return AjaxHelper.Seed(System.Net.HttpStatusCode.OK, "", new { token.Token });
        }
        /// <summary>
        /// 用户登出
        /// </summary>
        /// <returns></returns>
        [HttpGet("LoginOut")]
        public async Task<JsonResult> LoginOut()
        {
            await _userProvider.Clear();
            return AjaxHelper.Seed(System.Net.HttpStatusCode.OK, "");
        }

        #endregion

        #region User
        [HttpPost("Add")]
        public async Task<JsonResult> Add(User user)
        {
            return AjaxHelper.Seed(System.Net.HttpStatusCode.OK, "success");
        }
        #endregion
    }
}
