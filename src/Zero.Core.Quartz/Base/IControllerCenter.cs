using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.Quartz.Base
{
    /// <summary>
    /// quartz 抽象调度中心
    /// </summary>
    public interface IControllerCenter
    {
        Task<IScheduler> Center { get; }
        /// <summary>
        /// 启动任务调度
        /// </summary>
        /// <returns></returns>
        Task Start();
        /// <summary>
        /// 运行Job
        /// </summary>
        /// <returns></returns>
        Task RunJob();
        /// <summary>
        /// 暂停Job
        /// </summary>
        /// <returns></returns>
        Task PauseJob();
        /// <summary>
        /// 回复job
        /// </summary>
        /// <returns></returns>
        Task ResumeJob();
    }
}
