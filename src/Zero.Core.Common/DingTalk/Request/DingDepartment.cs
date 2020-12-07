using DingTalk.Api;
using DingTalk.Api.Request;
using DingTalk.Api.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Common.DingTalk.Config;

namespace Zero.Core.Common.DingTalk.Request
{
    /// <summary>
    /// 部门管理
    /// </summary>
    public class DingDepartment : DingAppConfig
    {
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="departId">父部门id（如果不传，默认部门为根部门，根部门ID为1）</param>
        public string GetDepartList(string departId)
        {
            IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/department/list");
            OapiDepartmentListRequest request = new OapiDepartmentListRequest();
            request.Id = departId;
            request.SetHttpMethod("GET");
            OapiDepartmentListResponse response = client.Execute(request, base.AccessToken);
            return response.Body;
        }

        /// <summary>
        /// 获取部门详情
        /// </summary>
        /// <param name="departId">部门id</param>
        /// <returns></returns>
        public string GetDepart(string departId)
        {
            IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/department/get");
            OapiDepartmentGetRequest request = new OapiDepartmentGetRequest();
            request.Id = departId;
            request.SetHttpMethod("Get");
            OapiDepartmentGetResponse response = client.Execute(request, base.AccessToken);
            return response.Body;
        }
    }
}
