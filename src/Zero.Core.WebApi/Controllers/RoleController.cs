using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zero.Core.Common.Result;
using Zero.Core.Domain.Dtos.Role;
using Zero.Core.IServices;

namespace Zero.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {

        readonly IRoleService _role;
        public RoleController(
            IRoleService role
            )
        {
            _role = role;
        }

        [HttpPost("GetDataList")]
        public async Task<JsonResult> GetDataList(RoleCondition condition)
        {
            return AjaxHelper.Seed(Ajax.Ok);
        }
    }
}
