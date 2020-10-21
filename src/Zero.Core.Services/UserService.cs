using System.Threading.Tasks;
using Zero.Core.Common.Helper;
using Zero.Core.Common.Units;
using Zero.Core.Common.User;
using Zero.Core.Domain.Dtos.User;
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
        readonly IJwtProvider _jwt;
        readonly ILogHelper _log;
        public UserService(
            IUserRepository repository,
            IUnitOfWork unit,
            IUserProvider userProvider,
            IJwtProvider jwt,
            ILogHelper log
            )
        {
            _repository = repository;
            _unit = unit;
            _userProvider = userProvider;
            _jwt = jwt;
            _log = log;
        }


        //public async Task<> Login(LoginDtos login)
        //{ 

        //}

        public void Info()
        {
            _log.Info("this  in userservice");
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
