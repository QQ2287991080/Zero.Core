﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Dtos.Role;
using Zero.Core.Domain.Entities;
using Zero.Core.IRepositories.Base;

namespace Zero.Core.IRepositories
{
    public interface IRoleRepository:IRepository<Role>
    {
        /// <summary>
        /// 获取角色id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<int>> GetRoleIds(int userId);
        /// <summary>
        /// 处理角色的菜单权限
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        Task RemoveMenu(CheckPermission check);
        /// <summary>
        /// 获取角色已存在的菜单id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<ExistsMenu> ExistsMenu(int roleId);
        /// <summary>
        /// 获取角色的权限信息
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        Task<IEnumerable<Permission>> RolesExistsPermission(IEnumerable<int> roleIds);
        /// <summary>
        /// 根据用户获取角色的名称
        /// 角色1,角色2,角色3......
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<string> GetRoleName(int userId);
    }
}
