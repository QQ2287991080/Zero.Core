using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.WebApi.StartupConfigExtensions
{
    public static class StaticFileExtension
    {
        public static void UseAvatar(this IApplicationBuilder app)
        {
            //初始化头像存储folder
            string avatar = AppContext.BaseDirectory + "avatar";
            if (!Directory.Exists(avatar))
            {
                Directory.CreateDirectory(avatar);
            }
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(avatar),
                RequestPath = "/avatar"
            });
        }
    }
}
