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
    public interface IRoleService:IBaseService<Role>,ISupportService
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<ListResult<Role>> GetDataList(RoleCondition condition);
    }
}
