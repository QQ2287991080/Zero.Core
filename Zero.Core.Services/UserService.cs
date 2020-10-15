using System.Threading.Tasks;
using Zero.Core.Common.User;
using Zero.Core.Domain.Entities;
using Zero.Core.IRepositories;
using Zero.Core.IRepositories.Base;
using Zero.Core.IServices;
using Zero.Core.Services.Base;

namespace Zero.Core.Services
{
    public class UserService : BaseService<User>,IUserService
    {
        readonly IUnitOfWork _unit;
        readonly IUserProvider _userProvider;
        public UserService(
            IUserRepository repository,
            IUnitOfWork unit,
            IUserProvider userProvider
            )
        {
            base._repository = repository;
            _unit = unit;
            _userProvider = userProvider;
        }


        public string UserName()
        {
            return _userProvider.UserName;
        }
        public async Task Tran()
        {
            await GetAllAsync();
        }
    }
}
