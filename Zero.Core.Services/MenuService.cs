using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities;
using Zero.Core.IRepositories;
using Zero.Core.IServices;
using Zero.Core.Services.Base;

namespace Zero.Core.Services
{
    public class MenuService:BaseService<Menu>,IMenuService
    {
        public MenuService(IMenuRepository menu)
        {
            _repository = menu;
        }
    }
}
