using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Dtos.Menu;
using Zero.Core.Domain.Entities;
using Zero.Core.IServices.Base;

namespace Zero.Core.IServices
{
    public interface IMenuService:IBaseService<Menu>,ISupportService
    {
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        Task<List<OutputMenu>> MenuTrees();
        /// <summary>
        /// 判断菜单名称是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistsMenuName(string name, int id = 0);

    }
}
