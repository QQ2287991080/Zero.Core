using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zero.Core.Common.Result;

namespace Zero.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilController : ControllerBase
    {
        /// <summary>
        /// 获取头像
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("GetAvatar")]
        public JsonResult GetAvatar(int pageIndex = 1, int pageSize = 10)
        {
            string path = AppContext.BaseDirectory + "avatar";
            string ip = this.Request.Host.ToUriComponent();
            //获取所有的图片
            string[] avatars = System.IO.Directory.GetFiles(path, "*.jpg");
            var paged = avatars.Skip(pageSize * (pageIndex - 1)).Take(pageSize);

            List<string> data = new List<string>();
            foreach (var item in paged)
            {
                string jpg = "http://" + ip + item.Replace(AppContext.BaseDirectory, "/");
                data.Add(@jpg);
            }
            return AjaxHelper.Seed(Ajax.Ok, data);
        }


        
    }
}
