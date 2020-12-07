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
    /// 用户管理
    /// </summary>
    public class DingUserManager : DingAppConfig
    {
        /// <summary>
        /// 分页获取部门用户
        /// </summary>
        /// <param name="departId"  cref="DingDepartment">部门id</param>
        /// <param name="offset">页码</param>
        /// <param name="size">页数</param>
        /// <param name="order" cref="UserOrder">排序规则</param>
        public string GetListByPage(long departId, long offset = 0, long size = 100, UserOrder order = UserOrder.custom)
        {
            IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/user/listbypage");
            OapiUserListbypageRequest request = new OapiUserListbypageRequest();
            request.DepartmentId = departId;
            request.Offset = offset;
            request.Size = size;
            request.Order = Enum.GetName(typeof(UserOrder), order);
            request.SetHttpMethod("GET");
            OapiUserListbypageResponse response = client.Execute(request, base.AccessToken);
            return response.Body;
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="userId"></param>
        public string GetUser(string userId)
        {
            IDingTalkClient client = new DefaultDingTalkClient("https://oapi.dingtalk.com/user/get");
            OapiUserGetRequest request = new OapiUserGetRequest();
            request.Userid = userId;
            request.SetHttpMethod("GET");
            OapiUserGetResponse response = client.Execute(request, base.AccessToken);
            return response.Body;
        }


        /// <summary>
        /// 支持分页查询，部门成员的排序规则，默认 是按自定义排序；
        ///entry_asc：代表按照进入部门的时间升序，
        ///entry_desc：代表按照进入部门的时间降序，
        ///modify_asc：代表按照部门信息修改时间升序，
        ///modify_desc：代表按照部门信息修改时间降序，
        ///custom：代表用户定义(未定义时按照拼音)排序
        /// </summary>
        public enum UserOrder
        {
            /// <summary>
            /// 按照进入部门的时间升序
            /// </summary>
            entry_asc = 1,
            /// <summary>
            /// 按照进入部门的时间降序
            /// </summary>
            entry_desc = 2,
            /// <summary>
            /// 按照部门信息修改时间升序
            /// </summary>
            modify_asc = 3,
            /// <summary>
            /// 按照部门信息修改时间降序
            /// </summary>
            modify_desc = 4,
            /// <summary>
            /// 用户定义(未定义时按照拼音)排序
            /// </summary>
            custom = 5
        }
    }
}
