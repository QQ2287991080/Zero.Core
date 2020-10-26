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
    public class PermissionService:BaseService<Permission>,IPermissionService
    {
        readonly IPermissionRepository _permission;
        public PermissionService(IPermissionRepository permission)
        {
            _repository = permission;
            _permission = permission;
        }


        public async Task<bool> IsExistsCode(string code, int id = 0)
        {
            if (id == 0)
                return await base.AnyAsync(a => a.Code == code);
            else
                return await base.AnyAsync(a => a.Code == code && a.Id != id);
        }
    }
}
