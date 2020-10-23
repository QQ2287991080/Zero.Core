using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Common.Extensions;

namespace Zero.Core.Common.Result
{
    public class AjaxHelper
    {
        public static JsonResult Seed(HttpStatusCode errCode, string errMsg, object data = null)
        {
            var result = new Result()
            {
                Data = data,
                ErrCode = errCode,
                ErrMsg = errMsg
            };
            return new JsonResult(result);
        }

        public static JsonResult Seed(Ajax errCode, string errMsg, object data = null)
        {
            var result = new Result()
            {
                Data = data,
                ErrCode = (HttpStatusCode)EnumExtension.GetValue(errCode),
                ErrMsg = string.IsNullOrEmpty(errMsg) ? EnumExtension.GetEnumDescription(errCode) : errMsg
            };
            return new JsonResult(result);
        }

        public static JsonResult Seed(Ajax errCode, object data = null)
        {
            var result = new Result()
            {
                Data = data,
                ErrCode = (HttpStatusCode)EnumExtension.GetValue(errCode),
                ErrMsg = EnumExtension.GetEnumDescription(errCode)
            };
            return new JsonResult(result);
        }

        public class Result
        {
            public string ErrMsg { get; set; }
            public HttpStatusCode ErrCode { get; set; }
            public object Data { get; set; }
        }
    }
    /// <summary>
    /// 错误码
    /// </summary>
    public enum Ajax
    { 
        [Description("操作成功")]
        Ok=200,
        [Description("请求失败")]
        Bad=400,
        [Description("请求的资源不存在")]
        Not=404,
        [Description("未授权")]
        Unauthorized=401
    }
   
}
