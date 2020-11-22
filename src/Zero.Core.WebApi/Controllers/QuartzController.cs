using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zero.Core.Common.Result;
using Zero.Core.Quartz.Base;

namespace Zero.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuartzController : ControllerBase
    {
        readonly IControllerCenter _center;
        public QuartzController(IControllerCenter center)
        {
            _center = center;
        }
        /// <summary>
        /// 开启任务调度
        /// </summary>
        /// <returns></returns>
        [HttpGet("Start")]
        public async Task<JsonResult> Start()
        {
            await _center.Start();
            return AjaxHelper.Seed(Ajax.Ok);
        }
        /// <summary>
        /// 运行Job
        /// </summary>
        /// <returns></returns>
        [HttpGet("Run")]
        public async Task<JsonResult> Run()
        {
            await _center.RunJob();
            return AjaxHelper.Seed(Ajax.Ok);
        }
        /// <summary>
        /// 暂停Job
        /// </summary>
        /// <returns></returns>
        [HttpGet("PauseJob")]
        public async Task<JsonResult> PauseJob()
        {
            await _center.PauseJob();
            return AjaxHelper.Seed(Ajax.Ok);
        }
        /// <summary>
        /// 恢复Job
        /// </summary>
        /// <returns></returns>
        [HttpGet("ResumeJob")]
        public async Task<JsonResult> ResumeJob()
        {
            await _center.ResumeJob();
            return AjaxHelper.Seed(Ajax.Ok);
        }
    }
}
