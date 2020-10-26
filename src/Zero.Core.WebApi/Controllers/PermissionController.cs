using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Annotations;
using Zero.Core.Common.Result;
using Zero.Core.Domain.Entities;
using Zero.Core.IServices;

namespace Zero.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [SwaggerTag("权限管理")]
    public class PermissionController : ControllerBase
    {
        readonly IPermissionService _permission;
        public PermissionController(IPermissionService permission)
        {
            _permission = permission;
        }

        /// <summary>
        /// 获取菜单的权限
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        [HttpGet("GetMenuPermission")]
        public async Task<JsonResult> GetMenuPermission(int menuId)
        {
            var list = await _permission.GetAllAsync(w => w.MenuId == menuId);
            return AjaxHelper.Seed(Ajax.Ok, list);
        }

        /// <summary>
        /// 验证权限编码重复
        /// </summary>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("IsExistCode")]
        public async Task<JsonResult> IsExistCode(string code, int id = 0)
        {
            bool any = await _permission.IsExistsCode(code, id);
            return AjaxHelper.Seed(Ajax.Ok, any);
        }

        /// <summary>
        /// 新增权限
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<JsonResult> Add(Permission permission)
        {
            if (!ModelState.IsValid)
            {
                var list = ModelState.SelectMany(s=>s.Value.Errors.Select(s=>s.ErrorMessage));
                return AjaxHelper.Seed(Ajax.Bad, list);
            }
            if (await _permission.IsExistsCode(permission.Code))
                return AjaxHelper.Seed(Ajax.Bad, "权限编码已存在！");
            var entity = await _permission.AddAsync(permission);
            return AjaxHelper.Seed(Ajax.Ok, entity);
        }
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        public async Task<JsonResult> Update(Permission permission)
        {
            if (!ModelState.IsValid)
            {
                var list = ModelState.SelectMany(s => s.Value.Errors.Select(s => s.ErrorMessage));
                return AjaxHelper.Seed(Ajax.Bad, list);
            }
            if (await _permission.IsExistsCode(permission.Code,permission.Id))
                return AjaxHelper.Seed(Ajax.Bad, "权限编码已存在！");
            await _permission.UpdateAsync(permission);
            return AjaxHelper.Seed(Ajax.Ok);
        }
        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Details")]
        public async Task<JsonResult> Details(int id)
        {
            var info = await _permission.FirstAsync(id);
            if (info == null)
                return AjaxHelper.Seed(Ajax.Bad, "权限已不存在！");
            return AjaxHelper.Seed(Ajax.Ok, info);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Delete")]
        public async Task<JsonResult> Delet(int id)
        {
            var info = await _permission.FirstAsync(id);
            if (info == null)
                return AjaxHelper.Seed(Ajax.Bad, "权限已不存在！");
            await _permission.DeleteAsync(id);
            return AjaxHelper.Seed(Ajax.Ok);
        }
    }
}
