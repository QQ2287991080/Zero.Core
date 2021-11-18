using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.Common.Helper
{
    /// <summary>
    /// 公共静态帮助类
    /// </summary>
    public  static  class CommonHelper
    {
      
        /// <summary>
        /// 获取加密盐
        /// </summary>
        /// <returns></returns>
        public static string GetSalt()
        {
            return RandomHelper.GetRandomNumberCore().ToString();
        }
        /// <summary>
        /// Md5加密
        /// </summary>
        /// <param name="pass"></param>
        /// <param name="slat"></param>
        /// <returns></returns>
        public static string MD5Encryption(string pass,string salt)
        {
#if NET6_0
            MD5 md5 = MD5.Create();
#else
            MD5 md5 = new MD5CryptoServiceProvider();
#endif
            
            //加密内容
            string content = pass + salt;
            //计算哈希值
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(content));
            //获取哈希值
            byte[] result = md5.Hash;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                builder.Append(result[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
