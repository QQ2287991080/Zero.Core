using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.Common.DingTalk.ConvertObj
{
    public abstract class BaseResult
    {
        /// <summary>
        /// 消息码
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string errmsg { get; set; }
    }
}
