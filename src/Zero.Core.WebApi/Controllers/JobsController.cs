using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Zero.Core.Common.Result;
using Zero.Core.Domain.Dtos.Jobs;
using Zero.Core.Domain.Entities;
using Zero.Core.Domain.Enums;
using Zero.Core.IServices;

namespace Zero.Core.WebApi.Controllers
{
    [SwaggerTag("任务管理")]
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        readonly IJobService _job;
        public JobsController(IJobService job)
        {
            _job = job;
        }

        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpPost("GetDataList")]
        public async Task<JsonResult> GetDataList(JobCondition condition)
        {
            var result = await _job.GetDataList(condition);
            return AjaxHelper.Seed(Ajax.Ok, result);
        }


        /// <summary>
        /// 判断名称是否重复
        /// </summary>
        /// <param name="name">任务名称</param>
        /// <param name="id">任务id</param>
        /// <returns></returns>
        [HttpGet("SameName")]
        public async Task<JsonResult> SameName(string name,int id=0)
        {
            bool ok = await _job.JobName(name, id);
            return AjaxHelper.Seed(Ajax.Ok, ok);
        }
        /// <summary>
        /// 新增任务
        /// </summary>
        /// <param name="jobs"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<JsonResult> Add(Jobs jobs)
        {
            if (await _job.JobName(jobs.Name))
                AjaxHelper.Seed(Ajax.Bad, "名称不能重复！");
            var entity = await _job.AddAsync(jobs);
            return AjaxHelper.Seed(Ajax.Ok, entity);
        }
        /// <summary>
        /// 更新任务信息
        /// </summary>
        /// <param name="jobs"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        public async Task<JsonResult> Update(Jobs jobs)
        {
            var info = await _job.FirstAsync(f => f.Id == jobs.Id);
            if (info == null)
                return AjaxHelper.Seed(Ajax.Bad, "任务已不存在！");
            if (await _job.JobName(jobs.Name,jobs.Id))
                AjaxHelper.Seed(Ajax.Bad, "名称不能重复！");
            info.Name = jobs.Name;
            info.AssemblyName = jobs.AssemblyName;
            info.ClassName = jobs.ClassName;
            info.EndTime = jobs.EndTime;
            info.JobGroup = jobs.JobGroup;
            info.JobKey = jobs.JobKey;
            info.Remark = jobs.Remark;
            info.StartTime = jobs.StartTime;
            info.TriggerKey = jobs.TriggerKey;
            info.Status = JobStatus.no;
             await _job.UpdateAsync(info);
            return AjaxHelper.Seed(Ajax.Ok);
        }
        /// <summary>
        /// 任务详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Details")]
        public async Task<JsonResult> Details(int id)
        {
            var info = await _job.FirstAsync(f => f.Id == id);
            if (info == null)
                return AjaxHelper.Seed(Ajax.Bad, "任务已不存在！");
            return AjaxHelper.Seed(Ajax.Ok, info);
        }
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Delete")]
        public async Task<JsonResult> Delete(int id)
        {
            var info = await _job.FirstAsync(f => f.Id == id);
            if (info == null)
                return AjaxHelper.Seed(Ajax.Bad, "任务已不存在！");
            if (info.Status != JobStatus.no)
                return AjaxHelper.Seed(Ajax.Bad, "删除任务必须是初始化状态！");
            await _job.DeleteAsync(info);
            return AjaxHelper.Seed(Ajax.Ok);
        }
    }
}
