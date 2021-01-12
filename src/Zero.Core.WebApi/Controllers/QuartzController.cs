using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Zero.Core.Common.Result;
using Zero.Core.IServices;
using Zero.Core.Quartz.QuartzCenter;

namespace Zero.Core.WebApi.Controllers
{
    [SwaggerTag("任务调度管理")]
    [Route("api/[controller]")]
    [ApiController]
    public class QuartzController : ControllerBase
    {
        readonly IControllerCenter _center;
        readonly IJobService _job;
        public QuartzController(
            IControllerCenter center,
            IJobService job
            )
        {
            _center = center;
            _job = job;
        }
        /// <summary>
        /// 开启任务调度
        /// </summary>
        /// <returns></returns>
        [HttpGet("Start")]
        public async Task<JsonResult> Start()
        {
            string message = await _center.Start();
            return AjaxHelper.Seed(Ajax.Ok,message);
        }
        /// <summary>
        /// 运行Job
        /// </summary>
        /// <returns></returns>
        [HttpGet("Run")]
        public async Task<JsonResult> Run(int id)
        {
            var job = await _job.FirstAsync(f => f.Id == id);
            if (job == null)
                return AjaxHelper.Seed(Ajax.Bad, "任务不存在！");
            var message= await _center.RunJob(job);
            return AjaxHelper.Seed(Ajax.Ok,message);
        }
        /// <summary>
        /// 暂停Job
        /// </summary>
        /// <returns></returns>
        [HttpGet("PauseJob")]
        public async Task<JsonResult> PauseJob(int id)
        {
            var job = await _job.FirstAsync(f => f.Id == id);
            if (job == null)
                return AjaxHelper.Seed(Ajax.Bad, "任务不存在！");
            string message = await _center.PauseJob(job);
            return AjaxHelper.Seed(Ajax.Ok,message);
        }
        /// <summary>
        /// 恢复Job
        /// </summary>
        /// <returns></returns>
        [HttpGet("ResumeJob")]
        public async Task<JsonResult> ResumeJob(int id)
        {
            var job = await _job.FirstAsync(f => f.Id == id);
            if (job == null)
                return AjaxHelper.Seed(Ajax.Bad, "任务不存在！");
            string message= await _center.ResumeJob(job);
            return AjaxHelper.Seed(Ajax.Ok, message);
        }
    }
}
