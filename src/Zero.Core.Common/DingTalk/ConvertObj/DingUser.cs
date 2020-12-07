using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.Common.DingTalk.ConvertObj
{
    public class DingUser:BaseResult
    {
        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool hasMore { get; set; }
        /// <summary>
        /// 用户列表
        /// </summary>
        public List<UserlistItem> userlist { get; set; }
    }

    public class UserlistItem
    {
        /// <summary>
        /// 员工在当前开发者企业账号范围内的唯一标识，系统生成，固定值，不会改变
        /// </summary>
        public string unionid { get; set; }
        /// <summary>
        /// 同unionId
        /// </summary>
        public string openId { get; set; }
        /// <summary>
        /// 员工在当前企业内的唯一标识，也称staffId。可由企业在创建时指定，并代表一定含义比如工号，创建后不可修改
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 是否为企业的老板
        /// </summary>
        public bool isBoss { get; set; }
        /// <summary>
        /// 所属部门列表
        /// </summary>
        public List<long> department { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 在部门的排序
        /// </summary>
        public long order { get; set; }
        /// <summary>
        /// 是否是主管
        /// </summary>
        public bool isLeader { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 用户是否激活了钉钉
        /// </summary>
        public string active { get; set; }
        /// <summary>
        /// 是否是企业的管理员
        /// </summary>
        public bool isAdmin { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string avatar { get; set; }
        /// <summary>
        /// 是否隐藏号码
        /// </summary>
        public string isHide { get; set; }
        /// <summary>
        /// 张力
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 国家地区码
        /// </summary>
        public string stateCode { get; set; }
    }

    

}
