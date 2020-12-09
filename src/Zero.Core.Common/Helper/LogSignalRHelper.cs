using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zero.Core.Common.Helper
{
    public class LogSignalRHelper
    {
        /// <summary>
        /// https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.readerwriterlockslim?view=netframework-4.8
        /// 这里主要控制控制多个线程读取日志文件
        /// </summary>
        static ReaderWriterLockSlim _slimLock = new ReaderWriterLockSlim();

        static LogSignalRHelper _helper = null;
        readonly object _lock = new object();
        public LogSignalRHelper()
        {
            //Console.WriteLine("LogSignalRHelper初始化！");
            lock (_lock)
            {
                if (_lock == null)
                {
                    Console.WriteLine("LogSignalRHelper 为空，实例化！");
                    _helper = new LogSignalRHelper();
                }
            }
        }
        public static List<SysExceptionData> Read(DateTime? date=null)
        {
            if (date == null)
                date = DateTime.Today;
            //日志对象集合
            List<SysExceptionData> datas = new List<SysExceptionData>();
            string filePath = AppContext.BaseDirectory + $"logs\\全局异常\\{date.Value:yyyyMMdd}.log";
           
            //判断日志文件是否存在
            if (!File.Exists(filePath))
            {
                filePath = Directory.GetCurrentDirectory() + $"\\logs\\全局异常\\{date.Value:yyyyMMdd}.log";
            }
            //判断文件是否存在
            if (!File.Exists(filePath))
            {
                return datas;
            }
            _slimLock.EnterReadLock();
            try
            {

                //获取日志文件流
                var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                //读取内容
                var reader = new StreamReader(fs);
                var content = reader.ReadToEnd();
                reader.Close();
                fs.Close();
                /*
                 *处理内容，换行符替换掉，然后在log4net配置文件中在每一写入日志结尾的地方加上 |
                 *这样做的好处是便于在读取日志文件的时候处理日志数据返回给客户端
                 *由于是在每一行结束的地方加上| 所有根据Split分割之后最后一个数据必然是空的
                 *所有Where去除一下。
                 */
                var contentList = content.Replace("\r\n", "").Split('|').Where(w => !string.IsNullOrEmpty(w));
                foreach (var item in contentList)
                {
                    //根据逗号分割单个日志数据的内容
                    var info = item.Split(',');
                    if (info.Count() < 5||info.FirstOrDefault()?.Contains("时间")==false)
                        continue;
                    //实例化日志对象
                    SysExceptionData data = new SysExceptionData();
                    data.CreateTime = Convert.ToDateTime(info[0].Split('：')[1]);
                    data.Level = info[2].Split('：')[1];
                    data.Summary = info[3].Split('：')[1];
                    data.UserName = info[4].Split('：')[1];
                    datas.Add(data);
                }
            }
            finally
            {
                //退出
                _slimLock.ExitReadLock();
            }
            return datas.OrderByDescending(bo => bo.CreateTime).ToList();
        }
    }
    public class SysExceptionData
    {
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 日志级别
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 日志描述
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 错误产生的用户
        /// </summary>
        public string UserName { get; set; }
    }
}
