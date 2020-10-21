using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
    }
    public class Result
    {
        public string ErrMsg { get; set; }
        public HttpStatusCode ErrCode { get; set; }
        public object Data { get; set; }
    }
}
