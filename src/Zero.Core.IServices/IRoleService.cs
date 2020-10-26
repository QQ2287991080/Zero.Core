using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Dtos;
using Zero.Core.Domain.Dtos.Role;
using Zero.Core.Domain.Entities;
using Zero.Core.IServices.Base;

namespace Zero.Core.IServices
{
    public interface IRoleService : IBaseService<Role>, ISupportService
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<ListResult<Role>> GetDataList(RoleCondition condition);
        /// <summary>
        /// 更新选中的菜单权限
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        Task CheckMenu(CheckPermission check);
        /// <summary>
        /// 获取角色已经选中的菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<ExistsMenu> GetRoleExistsMenu(int roleId);
        /// <summary>
        /// 验证名称
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistsName(string name, int id = 0);
        /// <summary>
        /// 获取当前所有能够使用的菜单和权限
        /// </summary>
        /// <returns>返回id和名称</returns>
        Task<List<IdName>> LoadMenuPermission();
    }
}
