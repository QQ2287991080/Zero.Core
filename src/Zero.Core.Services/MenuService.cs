using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Dtos;
using Zero.Core.Domain.Dtos.Menu;
using Zero.Core.Domain.Entities;
using Zero.Core.IRepositories;
using Zero.Core.IServices;
using Zero.Core.Services.Base;

namespace Zero.Core.Services
{
    public class MenuService:BaseService<Menu>,IMenuService
    {
        readonly IMenuRepository _menu;
        public MenuService(IMenuRepository menu)
        {
            _repository = menu;
            _menu = menu;
        }


        public async Task<bool> IsExistsMenuName(string name, int id = 0)
        {
            if (id == 0)
                return await base.AnyAsync(a => a.Name == name);
            else
                return await base.AnyAsync(a => a.Name == name && a.Id != id);
        }

        public async Task<List<OutputMenu>> MenuTrees()
        {
            var menus = await base.GetAllAsync(w => w.IsAllow == true);
            return _menu.ResolveMenu(menus);
        }
    }
}
