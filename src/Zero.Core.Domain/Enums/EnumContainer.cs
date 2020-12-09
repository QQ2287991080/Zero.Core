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
}
