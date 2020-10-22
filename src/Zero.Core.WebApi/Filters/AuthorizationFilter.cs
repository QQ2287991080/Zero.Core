using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Common.Redis;
using Zero.Core.Common.Result;
using Zero.Core.Common.User;

namespace Zero.Core.WebApi.Filters
{
    /// <summary>
    /// 权限认证
    /// </summary>
    public class AuthorizationFilter : IAsyncAuthorizationFilter
    {
        readonly IUserProvider _userProvider;
        public AuthorizationFilter(
            IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //此次请求的请求头信息
            var headers = context.HttpContext.Request.Headers;
            //根据请求头中authorization 获取token
            string token = _userProvider.GetToken(headers);
            //string token = _userProvider.Token;
            //验证token是否为空
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                //context.Result = AjaxHelper.Seed(System.Net.HttpStatusCode.Unauthorized, "授权失败");
            }
            else
            {
                //判断token 是否过期
                var userName = await RedisHelper.StringGetAsync(token);
                //判断用户名是否为空
                if (string.IsNullOrEmpty(userName))
                {
                    context.Result = AjaxHelper.Seed(System.Net.HttpStatusCode.Unauthorized, "token time out，please refresh");
                }
            }
        }
    }

    /// <summary>
    /// 自定义接口实现，排除有AllowAnonymousAttribute的接口
    /// </summary>
    public class CustomAuthorizeFilter : IActionModelConvention
    {
        readonly IUserProvider _userProvider;
        public CustomAuthorizeFilter(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }
        public void Apply(ActionModel action)
        {
            //如果接口没有使用特性，就加上我的自定义身份证认证过滤器
            if (!action.Attributes.Any(a => a is AllowAnonymousAttribute))
            {
                if (!action.Controller.Attributes.Any(a => a is AllowAnonymousAttribute))
                {
                    action.Filters.Add(new AuthorizationFilter(_userProvider));
                }
            }
        }
    }
}
