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
    }
}
