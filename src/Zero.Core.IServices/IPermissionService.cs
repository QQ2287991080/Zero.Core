using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities;
using Zero.Core.IServices.Base;

namespace Zero.Core.IServices
{
    public interface IPermissionService : IBaseService<Permission>,ISupportService
    {
        /// <summary>
        /// 验证权限编码是不是唯一
        /// </summary>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistsCode(string code, int id = 0);
    }
}
