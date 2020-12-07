using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zero.Core.Common.DingTalk;
using Zero.Core.Common.Result;

namespace Zero.Core.WebApi.Controllers.test
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDingController : ControllerBase
    {
        readonly IDingTalkHandler _dingTalk;
        public TestDingController(IDingTalkHandler dingTalk)
        {
            _dingTalk = dingTalk;
        }

        [HttpGet("getDepartment")]
        public JsonResult GetDepartment(string departId)
        {
            var result = _dingTalk.Department.GetDepart(departId);
            return AjaxHelper.Seed(Ajax.Ok);
        }

        [HttpGet("getUserList")]
        public JsonResult GetUserList(string departId)
        {
            bool hasMore = true;
            int offset = 0;
            int size = 1;
            List<Common.DingTalk.ConvertObj.UserlistItem> items = new List<Common.DingTalk.ConvertObj.UserlistItem>();
            while (hasMore)
            {
                var result = _dingTalk.User.GetListByPage(Convert.ToInt64(departId), offset, size);
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Common.DingTalk.ConvertObj.DingUser>(result);
                hasMore = data.hasMore;
                items.AddRange(data.userlist);
                offset++;
                size++;
            }
            return AjaxHelper.Seed(Ajax.Ok, items);
        }
    }
}
