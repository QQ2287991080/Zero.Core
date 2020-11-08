using System.Collections.Generic;
using System.Threading.Tasks;
using Zero.Core.Domain.Dtos;
using Zero.Core.Domain.Dtos.User;
using Zero.Core.Domain.Entities;
using Zero.Core.IServices.Base;

namespace Zero.Core.IServices
{
    public interface IUserService:IBaseService<User>,ISupportService
    {
       
        /// <summary>
        /// 判断用户名是否已经存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> IsUserNameExists(string userName, int id = 0);
        /// <summary>
        /// 获取用户分页数据
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<ListResult<User>> GetDataList(UserCondition condition);
        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <returns></returns>
        Task<UserInfo> GetUserInfo();
        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <returns></returns>
        Task<User> GetCenterInfo();
        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task SetRole(int userId, int roleId);
        /// <summary>
        /// 移除用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task RemoveRole(int userId, int roleId);
        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<int>> GetUserRole(int userId);

        
    }
}
