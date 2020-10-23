using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Dtos;
using Zero.Core.Domain.Dtos.Role;
using Zero.Core.Domain.Entities;
using Zero.Core.IRepositories;
using Zero.Core.IServices;
using Zero.Core.Services.Base;

namespace Zero.Core.Services
{
    public class RoleService:BaseService<Role>,IRoleService
    {
        readonly IRoleRepository _role;
        public RoleService(IRoleRepository role)
        {
            _repository = role;
            _role = role;
        }



        public async Task<ListResult<Role>> GetDataList(RoleCondition condition)
        {
            Expression<Func<Role, bool>> where = w =>
            string.IsNullOrEmpty(condition.Name) || w.Name.Contains(condition.Name);
            var result= await base.GetPageAsync(where, w => w.CreateTime, condition.PageIndex, condition.PageIndex);
            return new ListResult<Role>(condition.PageIndex, condition.PageSize, result.Item1, result.Item2);
        }


        public async Task CheckMenu(CheckPermission check)
        {
            if (check.MenuIds.IsNullOrEmpty())
            {
                await _role.RemoveMenu(check);
            }
        }

        public async Task<List<int>> GetRoleExistsMenu(int roleId)
        {
            return await _role.ExistsMenu(roleId);
        }

        public async Task<bool> IsExistsName(string name, int id = 0)
        {
            if (id == 0)
                return await base.AnyAsync(a => a.Name == name);
            else
                return await base.AnyAsync(a => a.Name == name && a.Id != id);
        }
    }
}
