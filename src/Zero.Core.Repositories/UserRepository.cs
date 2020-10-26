using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities;
using Zero.Core.IRepositories;
using Zero.Core.IRepositories.Base;
using Zero.Core.Repositories.Basee;

namespace Zero.Core.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        readonly IUnitOfWork _unit;
        public UserRepository(IUnitOfWork unit) : base(unit)
        {
            _unit = unit;
        }


        public async Task SetUserRole(int userId,int roleId)
        {
            var db = _unit.DbContext;
            await db.UserRoles.AddAsync(new UserRole { RoleId = roleId, UserId = userId });
            await _unit.SaveAsync();
        }
        public async Task RemoveUserRole(int userId,int roleId)
        {
            var db = _unit.DbContext;
            await db.Database.ExecuteSqlRawAsync($"Delete from T_UserRole where UserId={userId} and RoleId={roleId}");
        }

        public async Task<List<int>> GetUserRole(int userId)
        {
            var db = _unit.DbContext;
            var user = await db.UserRoles
                .Where(w => w.UserId == userId)
                .Select(s => s.RoleId)
                .ToListAsync();
            return user;
        }
    }
}
