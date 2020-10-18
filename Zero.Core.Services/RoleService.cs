using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities;
using Zero.Core.IRepositories;
using Zero.Core.IServices;
using Zero.Core.Services.Base;

namespace Zero.Core.Services
{
    public class RoleService:BaseService<Role>,IRoleService
    {
        public RoleService(IRoleRepository role)
        {
            _repository = role;
        }
    }
}
