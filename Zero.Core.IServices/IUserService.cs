using Zero.Core.Domain.Entities;
using Zero.Core.IServices.Base;

namespace Zero.Core.IServices
{
    public interface IUserService:IBaseService<User>,ISupportService
    {
        string UserName();
        void Info();
    }
}
