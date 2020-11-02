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
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<JsonResult> Add(Dictionaries model)
        {
            var entity = await _dic.AddAsync(model);
            return AjaxHelper.Seed(Ajax.Ok, entity);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        public async Task<JsonResult> Update(Dictionaries model)
        {
            await _dic.UpdateAsync(model);
            return AjaxHelper.Seed(Ajax.Ok);
        }
    }
}
