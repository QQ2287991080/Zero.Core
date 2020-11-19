using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.Tasks.Base
{
    /// <summary>
    /// 抽象处理任务类
    /// </summary>
    public interface IExample
    {
        /// <summary>
        /// 启动调度
        /// </summary>
        /// <returns></returns>
        Task Start();
        /// <summary>
        /// 运行一个job
        /// </summary>
        /// <returns></returns>
        Task Run();
        /// <summary>
        /// 暂停工作
        /// </summary>
        /// <returns></returns>
        Task PauseJob();
        /// <summary>
        /// 恢复Job
        /// </summary>
        /// <returns></returns>
        Task ResumeJob();
       
    }
}
