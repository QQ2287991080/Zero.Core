using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Zero.Core.Common.Result;
using Zero.Core.Domain.Entities;
using Zero.Core.IServices;

namespace Zero.Core.WebApi.Controllers
{
    /// <summary>
    /// 字典数据
    /// </summary>
    [Route("api/[controller]")]
    [SwaggerTag("字典数据管理")]
    public class DictionariesController : ControllerBase
    {
        readonly IDictionariesService _dic;
        public DictionariesController(
            IDictionariesService dictionaries)
        {
            _dic = dictionaries;
        }


        /// <summary>
        /// 获取字典树
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDicTree")]
        public async Task<JsonResult> GetDicTree()
        {
            var data= await _dic.GetTree();
            return AjaxHelper.Seed(Ajax.Ok, data);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<JsonResult> Add(Dictionaries model)
        {
            if (await _dic.NameContains(model.Name, model.IdParent))
                return AjaxHelper.Seed(Ajax.Bad, "在当前级别字典数据中已存在该名称！");
            var entity = await _dic.AddAsync(model);
            return AjaxHelper.Seed(Ajax.Ok, entity);
        }
        /// <summary>
        /// 验证名称的重复
        /// </summary>
        /// <param name="name"></param>
        /// <param name="idParent"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("IsContains")]
        public async Task<JsonResult> IsContains(string name, int? idParent, int id = 0)
        {
            var ok = await _dic.NameContains(name, idParent, id);
            return AjaxHelper.Seed(Ajax.Ok, ok);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        public async Task<JsonResult> Update(Dictionaries model)
        {
            var info = await _dic.FirstAsync(model.Id);
            if (info == null)
                return AjaxHelper.Seed(Ajax.Bad, "当前字典数据已不存在！");
            if (await _dic.NameContains(model.Name, model.IdParent,model.Id))
                return AjaxHelper.Seed(Ajax.Bad, "在当前级别字典数据中已存在该名称！");

            info.Name = model.Name;
            info.IdParent = model.IdParent;
            info.Sort = model.Sort;
            info.Sort = model.Sort;
            await _dic.UpdateAsync(model);
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
            var info = await _dic.FirstAsync(id);
            if (info == null)
                return AjaxHelper.Seed(Ajax.Bad, "当前字典数据已不存在！");
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
            var info = await _dic.FirstAsync(id);
            if (info == null)
                return AjaxHelper.Seed(Ajax.Bad, "当前字典数据已不存在！");
            await _dic.DeleteAsync(info);
            return AjaxHelper.Seed(Ajax.Ok);
        }
    }
}
