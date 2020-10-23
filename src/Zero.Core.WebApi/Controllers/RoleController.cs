using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Zero.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {

        [HttpPost("GetDataList")]
        public async Task<JsonResult> GetDataList()
        {

        }
    }
}
