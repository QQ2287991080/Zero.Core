using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Zero.Core.Common.Result;
using Zero.Core.Domain.Dtos.Role;
using Zero.Core.Domain.Entities;
using Zero.Core.IServices;

namespace Zero.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [SwaggerTag("角色权限管理")]
    public class RoleController : ControllerBase
    {

        readonly IRoleService _role;
        public RoleController(
            IRoleService role
            )
        {
            _role = role;
        }

        /// <summary>
        /// 获取当前所有能够使用的菜单和权限
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMenuPermission")]
        public async Task<JsonResult> GetMenuPermission()
        {
            var data = await _role.LoadMenuPermission();
            return AjaxHelper.Seed(Ajax.Ok, data);
        }
        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetRoleExistPermission")]
        public async Task<JsonResult> GetRoleExistPermission(int id)
        {
            var data = await _role.GetRoleExistsMenu(id);
            return AjaxHelper.Seed(Ajax.Ok, data);
        }
        /// <summary>
        /// 设置角色权限<see cref="CheckPermission"/>
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        [HttpPost("SetPermission")]
        public async Task<JsonResult> SetPermission(CheckPermission check)
        {
            await _role.CheckMenu(check);
            return AjaxHelper.Seed(Ajax.Ok);
        }
        #region Role

        /// <summary>
        /// 获取角色列表部分字段
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetShortDataList")]
        public async Task<JsonResult> GetShortDataList()
        {
            var list = await _role.GetAllAsync();
            var data = list.Select(s => new { s.Id, s.Name,Checked=false });
            return AjaxHelper.Seed(Ajax.Ok, data);
        }
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpPost("GetDataList")]
        public async Task<JsonResult> GetDataList(RoleCondition condition)
        {
            var list = await _role.GetDataList(condition);
            return AjaxHelper.Seed(Ajax.Ok, list);
        }
        /// <summary>
        /// 验证角色名称
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("IsExistsName")]
        public async Task<JsonResult> IsExistsName(string name,int id=0)
        {
            bool any = await _role.IsExistsName(name, id);
            return AjaxHelper.Seed(Ajax.Ok, any);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<JsonResult> Add(Role role)
        {
            if (await _role.IsExistsName(role.Name))
                return AjaxHelper.Seed(Ajax.Bad, "角色名已存在！");
            var entity = await _role.AddAsync(role);
            return AjaxHelper.Seed(Ajax.Ok, entity);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        public async Task<JsonResult> Update(Role role)
        {
            if (await _role.IsExistsName(role.Name,role.Id))
                return AjaxHelper.Seed(Ajax.Bad, "角色名已存在！");
            await _role.UpdateAsync(role);
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
            var info = await _role.FirstAsync(id);
            if (info == null)
                return AjaxHelper.Seed(Ajax.Bad, "角色已不存在！");
            return AjaxHelper.Seed(Ajax.Ok, info);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Delete")]
        public async Task<JsonResult> Delete(int id)
        {
            var info = await _role.FirstAsync(id);
            if (info == null)
                return AjaxHelper.Seed(Ajax.Bad, "角色已不存在！");
            await _role.DeleteAsync(id);
            return AjaxHelper.Seed(Ajax.Ok, info);
        }
        #endregion
    }
}
