using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Zero.Core.Domain.Enums
{
    class EnumContainer
    {
    }

    /// <summary>
    /// 图标类型
    /// </summary>
    public enum IconType
    {
        [Description("Element-UI")]
        el = 1,
        [Description("SVG")]
        svg = 2
    }

    /// <summary>
    /// 任务的状态
    /// </summary>
    public enum JobStatus
    {
        /// <summary>
        /// 初始化
        /// </summary>
        [Description("初始化")]
        no = 1,
        /// <summary>
        /// 运行中
        /// </summary>
        [Description("运行中")]
        running = 2,
        /// <summary>
        /// 暂停
        /// </summary>
        [Description("暂停")]
        stop = 3,
        /// <summary>
        /// 异常
        /// </summary>
        [Description("异常")]
        error = 4
    }

    /// <summary>
    /// 触发器时间的形式
    /// </summary>
    public enum TriggerInterval
    {
        /// <summary>
        /// 秒
        /// </summary>
        [Description("秒")]
        seconds=1,
        /// <summary>
        /// 分
        /// </summary>
        [Description("分")]
        minutes=2,
        /// <summary>
        /// 时
        /// </summary>
        [Description("时")]
        hours=3
    }
    /// <summary>
    /// 触发器类型
    /// 如果您需要一个基于类似于日历的概念而不是根据SimpleTrigger的确切间隔重复发生的工作解雇时间表，则CronTriggers通常比SimpleTrigger有用。
    /// 如果您需要在特定的时间或特定的时间准确执行一次作业，然后在特定的时间间隔重复执行一次，SimpleTrigger应该可以满足您的调度需求。
    /// </summary>
    public enum TriggerType
    {
        /// <summary>
        /// simple
        /// </summary>
        simple = 1,
        /// <summary>
        /// cron
        /// </summary>
        cron = 2
    }
}
