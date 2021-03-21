using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Zero.Core.Common.Helper
{
    public class FileHelper
    {
        /// <summary>
        /// 创建文件，常用创建本地图片。
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="vitualBasePath">路径</param>
        /// <returns>
        /// <param>文件是否创建成功</param>
        /// <param>文件虚拟路径</param>
        /// <param>文件物理路径</param>
        /// <param>文件md5值</param>
        /// </returns>
        public static Tuple<bool,string,string,string> Create(Stream stream,string fileName,string vitualBasePath= "avatar\\")
        {
            //运行路径
            string basePath = AppContext.BaseDirectory;
            //后缀名
            string suf = Path.GetExtension(fileName);
            //文件名
            //string guid = Guid.NewGuid().ToString();
            //md5值
            string md5 = FileMD5String(stream);
            //可用Combine拼接路径
            //string t = Path.Combine(vitualBasePath, md5+suf);
            //虚拟路径
            string vitualPath = vitualBasePath + md5 + suf;
            //物理路径
            string physicsPath = basePath + vitualPath;
            //文件夹
            string folder = basePath + vitualBasePath;
            //文件夹是否存在不存在创建
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            bool success = false;
            //创建文件
            if (!File.Exists(physicsPath))
            {
                using (var fs = File.Create(physicsPath))
                {
                    //stream.CopyTo(fs);
                    success = true;
                }
            }
            return new Tuple<bool, string, string, string>(success, vitualBasePath, physicsPath, md5);
        }

        /// <summary>
        /// 获取文件的MD5值
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string FileMD5String(Stream stream)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] md5Byte = md5.ComputeHash(stream);
                return BitConverter.ToString(md5Byte).Replace("-", string.Empty);
            }
        }
    }
}
