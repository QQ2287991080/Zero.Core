using Zero.Core.Domain.Entities;
using Zero.Core.IRepositories;
using Zero.Core.IRepositories.Base;
using Zero.Core.Repositories.Basee;

namespace Zero.Core.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository, ISupportRepository
    {
        public UserRepository(IUnitOfWork unit) : base(unit)
        {
        }
    }
}
