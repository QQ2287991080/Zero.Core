using AutoMapper;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Zero.Core.Common.Helper;
using Zero.Core.Common.Redis;
using Zero.Core.Common.Units;
using Zero.Core.Common.User;
using Zero.Core.Domain.Dtos;
using Zero.Core.Domain.Dtos.Menu;
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
        readonly IUserRepository _user;
        readonly IUnitOfWork _unit;
        readonly IUserProvider _userProvider;
        readonly ILogHelper _log;
        readonly IRoleRepository _role;
        readonly IMenuRepository _menu;
        readonly IMapper _mapper;
        public UserService(
            IUserRepository repository,
            IUnitOfWork unit,
            IUserProvider userProvider,
            ILogHelper log,
            IRoleRepository role,
            IMenuRepository menu,
            IMapper mapper
            )
        {
            _repository = repository;
            _unit = unit;
            _userProvider = userProvider;
            _log = log;
            _role = role;
            _menu = menu;
            _mapper = mapper;
            _user = repository;
        }

        public async Task<ListResult<User>> GetDataList(UserCondition condition)
        {
            Expression<Func<User, bool>> where = PredicateExtensions.True<User>();
            if (!string.IsNullOrEmpty(condition.UserName))
                where = where.And(a => a.UserName.Contains(condition.UserName));
            if (!string.IsNullOrEmpty(condition.Phone))
                where = where.And(a => a.Phone.Contains(condition.Phone));
            var list = await base.GetPageAsync(where, w => w.CreateTime, condition.PageIndex, condition.PageSize);
            foreach (var item in list.Item2)
            {
                item.RoleStr = await _role.GetRoleName(item.Id);
            }
            return new ListResult<User>(condition.PageIndex, condition.PageSize, list.Item1, list.Item2);
        }

        public async Task<bool> IsUserNameExists(string userName, int id = 0)
        {
            if (id == 0)
                return await base.AnyAsync(f => f.UserName == userName);
            else
                return await base.AnyAsync(f => f.UserName == userName && f.Id != id);
        }

        public async Task SetRole(int userId, int roleId)
        {
            await _user.SetUserRole(userId, roleId);
        }

        public async Task RemoveRole(int userId, int roleId)
        {
            await _user.RemoveUserRole(userId, roleId);
        }

        public async Task<List<int>> GetUserRole(int userId)
        {
            return await _user.GetUserRole(userId);
        }
        public async Task<UserInfo> GetUserInfo()
        {
            var userName = _userProvider.UserName;
            var user = await base.FirstAsync(f => f.UserName == userName);
            //角色信息
            var refRoles = await _role.GetRoleIds(user.Id);
            var roles = await _role.GetAllAsync(w => refRoles.Contains(w.Id));
            var roleStr = await _role.GetRoleName(user.Id);
            //菜单信息
            var menus = await _menu.GetRolesMenu(refRoles);
            var menuUrls = (await _menu.GetAllAsync(w => menus.Select(s => s.MenuId).Contains(w.Id) && w.Url != "/")).Select(s => s.Url);
            //权限信息
            var permission = await _role.RolesExistsPermission(refRoles);

            UserInfo userInfo = new UserInfo()
            {
                IsSuperAdmin = roles.Any(a => a.Name == "SuperAdmin"),
                Role = roleStr,
                Avatar = user.Avatar,
                Menu = await ResolveMenu(menus),
                MenuUrls = menuUrls,
                PermissionCode = permission.Select(s => s.Code),
                RealName = user.RealName,
                Remark = user.Remark,
                UserId = user.Id,
                UserName = user.UserName,
            };
            return userInfo;
        }
        async Task<List<OutputMenu>> ResolveMenu(IEnumerable<RoleMenu> menus)
        {
            var roleMenus = menus.Select(s => s.MenuId);
            //获取所有的菜单
            var allMenu = await _menu.GetAllAsync(w => w.IsAllow == true, ob => ob.Sort, true);
            //所有的父级菜单
            var allParent = allMenu.Where(w => w.IdParent is null);
            var outputModel = new List<OutputMenu>();
            var result = new List<OutputMenu>();

            foreach (var item in allMenu)
            {
                if (roleMenus.Contains(item.Id))
                {
                    outputModel.Add(_mapper.Map<OutputMenu>(item));
                }
            }
            if (allParent != null && allParent.Count() > 0)
            {
                foreach (var item in allParent)
                {
                    if (roleMenus.Contains(item.Id))
                    {
                        var model = _mapper.Map<OutputMenu>(item);
                        OutputMenu.Resolver(outputModel, model);
                        result.Add(model);
                    }
                }
            }
            //如果父级菜单没有就输出已存在的菜单
            if (!result.IsNullOrEmpty())
            {
                return outputModel;
            }
            return result;
        }
    }
}
