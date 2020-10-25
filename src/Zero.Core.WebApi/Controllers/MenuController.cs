using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Zero.Core.Common.Result;
using Zero.Core.Domain.Entities;
using Zero.Core.IServices;

namespace Zero.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [SwaggerTag("菜单管理")]
    [AllowAnonymous]
    public class MenuController : ControllerBase
    {
        readonly IMenuService _menu;
        public MenuController(
            IMenuService menu
            )
        {
            _menu = menu;
        }

        /// <summary>
        /// 返回所有可用菜单，用于角色权限操作
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllMenu")]
        public async Task<JsonResult> GetAllMenu()
        {
            var list = await _menu.GetAllAsync(w => w.IsAllow == true);
            var data = list.Select(s => new { s.Id, s.Name });
            return AjaxHelper.Seed(Ajax.Ok, data);
        }
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMenuTree")]
        public async Task<JsonResult> GetMenuTree()
        {
            var tree = await _menu.MenuTrees();
            return AjaxHelper.Seed(Ajax.Ok, tree);
        }

        /// <summary>
        /// 验证菜单名称是否已经存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("IsExistsName")]
        public async Task<JsonResult> IsExistsName(string name, int id = 0)
        {
            var any = await _menu.IsExistsMenuName(name, id);
            return AjaxHelper.Seed(Ajax.Ok, any);
        }
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<JsonResult> Add(Menu menu)
        {
            if (await _menu.IsExistsMenuName(menu.Name))
                return AjaxHelper.Seed(Ajax.Bad, "菜单名称已存在");
            var entity = await _menu.AddAsync(menu);
            return AjaxHelper.Seed(Ajax.Ok, entity);
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        public async Task<JsonResult> Update(Menu menu)
        {
            var info = await _menu.FirstAsync(f => f.Id == menu.Id);
            if (info == null)
                return AjaxHelper.Seed(Ajax.Bad, "菜单已不存在，请刷新！");
            if (await _menu.IsExistsMenuName(menu.Name,menu.Id))
                return AjaxHelper.Seed(Ajax.Bad, "菜单名称已存在");
            await _menu.UpdateAsync(menu);
            return AjaxHelper.Seed(Ajax.Ok);
        }

        /// <summary>
        /// 菜单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Details")]
        public async Task<JsonResult> Details(int id)
        {
            var info = await _menu.FirstAsync(f => f.Id == id);
            if (info == null)
                return AjaxHelper.Seed(Ajax.Bad, "菜单已不存在，请刷新！");
            return AjaxHelper.Seed(Ajax.Ok, info);
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Delete")]
        public async Task<JsonResult> Delete(int id)
        {
            var info = await _menu.FirstAsync(f => f.Id == id);
            if (info == null)
                return AjaxHelper.Seed(Ajax.Bad, "菜单已不存在，请刷新！");
            await _menu.DeleteAsync(info);
            return AjaxHelper.Seed(Ajax.Ok);
        }
    }
}
