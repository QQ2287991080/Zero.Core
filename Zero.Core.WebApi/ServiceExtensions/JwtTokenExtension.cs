using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Common.Helper;
using Zero.Core.Common.Units;

namespace Zero.Core.WebApi.ServiceExtensions
{
    public static class JwtTokenExtension
    {
        public static IServiceCollection AddJwtToken(this IServiceCollection services)
        {
            //使用jwt 定义的规则，禁用.net core
            //JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
            var jwt = AppsettingHelper.Get<JwtToken>("JWT");
            if (jwt == null)
            {
                Console.WriteLine("appsetting.json文件没有 JWT相关配置，请检查！");
                return services;
            }
            if (string.IsNullOrEmpty(jwt.ValidAudience) 
                || string.IsNullOrEmpty(jwt.ValidIssuer) 
                || string.IsNullOrEmpty(jwt.SecurityKey))
            {
                Console.WriteLine("Jwt配置错误错误，请检查appsetting.json文件！");
                return services;
            }

            //添加jwt验证：
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    RequireExpirationTime = true,//必须具有“过期”值。
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ClockSkew = TimeSpan.FromDays(jwt.Time),//设置时间
                    ValidAudience = jwt.ValidAudience,//Audience
                    ValidIssuer =jwt.ValidIssuer,//Issuer，这两项和前面签发jwt的设置一致
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecurityKey))//拿到SecurityKey
                };
            });

            //swagger 替换 core 内置 system.text.json
            services.AddSwaggerGenNewtonsoftSupport();
            return services;
        }

       
    }
}
