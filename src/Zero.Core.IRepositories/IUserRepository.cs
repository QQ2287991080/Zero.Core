using System.Collections.Generic;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities;
using Zero.Core.IRepositories.Base;

namespace Zero.Core.IRepositories
{
    public interface IUserRepository:IRepository<User>
    {
        /// <summary>
        /// 新增用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task SetUserRole(int userId, int roleId);
        /// <summary>
        /// 移除用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task RemoveUserRole(int userId, int roleId);
        /// <summary>
        /// 获取用户绑定的角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<int>> GetUserRole(int userId);
    }
}
