using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Zero.Core.Common.Helper
{
    public class AppsettingHelper
    {
        static IConfiguration _configuration;
        static readonly string _fileName = "appsettings.json";
        public AppsettingHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public AppsettingHelper()
        {
            //在根目录或者bin目录寻找文件
            var directory = AppContext.BaseDirectory;//获取程序bin目录
            directory = directory.Replace("\\", "/");//转换字符
            var filePath = $"{directory}/{_fileName}";//拼接文件路径
            if (!File.Exists(filePath))//如果文件不存在
            {
                var length = directory.IndexOf("/bin");//截掉bin目录，拼接根目录
                filePath = $"{directory.Substring(0, length)}/{_fileName}";
            }
            _configuration = new ConfigurationBuilder()
                .AddJsonFile(filePath, true, true).Build();
        }
        /// <summary>
        /// 根据节点获取值
        /// 例子：多层节点下的值节点1:节点2:节点3......
        /// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1
        /// </summary>
        /// <param name="sections"></param>
        /// <returns></returns>
        public static string Get(params string[] sections)
        {
            string key = string.Join(":", sections);
            return _configuration[key];
        }

        /// <summary>
        /// 获取配置文件，赋值对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sections"></param>
        /// <returns></returns>
        public static T Get<T>(params string[] sections)
        {
            string key = string.Join(":", sections);
            var instance = _configuration.GetSection(key).Get<T>();
            return instance;
        }

    }
}
