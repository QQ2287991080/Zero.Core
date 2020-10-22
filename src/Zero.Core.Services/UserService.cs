using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Zero.Core.Common.Helper;
using Zero.Core.Common.Units;
using Zero.Core.Common.User;
using Zero.Core.Domain.Dtos;
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
        readonly ILogHelper _log;
        readonly IRoleRepository _role;
        readonly IMenuRepository _menu;
        public UserService(
            IUserRepository repository,
            IUnitOfWork unit,
            IUserProvider userProvider,
            ILogHelper log,
            IRoleRepository role,
            IMenuRepository menu
            )
        {
            _repository = repository;
            _unit = unit;
            _userProvider = userProvider;
            _log = log;
            _role = role;
            _menu = menu;
        }

        public async Task<ListResult<User>> GetDataList(UserCondition condition)
        {
            Expression<Func<User, bool>> where = PredicateExtensions.True<User>();
            if (string.IsNullOrEmpty(condition.UserName))
                where = where.And(a => a.UserName.Contains(condition.UserName));
            if (string.IsNullOrEmpty(condition.Phone))
                where = where.And(a => a.Phone.Contains(condition.Phone));
            var list = await base.GetPageAsync(where, w => w.CreateTime, condition.PageIndex, condition.PageSize);
            return new ListResult<User>(condition.PageIndex, condition.PageSize, list.Item1, list.Item2);
        }

        public async Task<bool> IsUserNameExists(string userName, int id = 0)
        {
            if (id == 0)
                return await base.AnyAsync(f => f.UserName == userName);
            else
                return await base.AnyAsync(f => f.UserName == userName && f.Id != id);
        }



        public async Task<UserInfo> GetUserInfo()
        {
            await _unit.BeginAsync();
            //用户名
            try
            {
                var userName = _userProvider.UserName;
                var user = await base.FirstAsync(f => f.UserName == userName);
                //角色信息
                var refRoles = await _role.GetRoleIds(user.Id);
                var roles = await _role.GetAllAsync(w => refRoles.Contains(w.Id));
                //菜单信息
                var menus = await _menu.GetRolesMenu(refRoles);

                UserInfo userInfo = new UserInfo()
                {
                    Avatar = user.Avatar,
                    Menu = menus,
                    RealName = user.RealName,
                    Remark = user.Remark,
                    UserId = user.Id,
                    UserName = user.UserName,

                };
               await _unit.CommitAsync();
                return userInfo;
            }
            catch (Exception ex)
            {
                await _unit.RollbackAsync();
                throw ex;
            }
        }
    }
}
