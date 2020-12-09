using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities;

namespace Zero.Core.Quartz.QuartzCenter
{
    /// <summary>
    /// 抽象任务调度中心
    /// </summary>
    public interface IControllerCenter
    {
        Task<IScheduler> Center { get; }
        /// <summary>
        /// 启动任务调度
        /// </summary>
        /// <returns></returns>
        Task<string> Start();
        /// <summary>  
        /// 运行Job
        /// </summary>
        /// <returns></returns>
        Task<string> RunJob(Jobs jobs);
        /// <summary>
        /// 暂停Job
        /// </summary>
        /// <returns></returns>
        Task<string> PauseJob(Jobs jobs);
        /// <summary>
        /// 恢复job
        /// </summary>
        /// <returns></returns>
        Task<string> ResumeJob(Jobs jobs);
    }

    
}
