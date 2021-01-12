using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities.Base;
using Zero.Core.Domain.Enums;

namespace Zero.Core.Domain.Entities
{
    /// <summary>
    /// Quartz任务管理表
    /// </summary>
    public class Jobs: Entity
    {

        public Jobs()
        {
            this.Status = JobStatus.no;
            this.TriggerInterval = TriggerInterval.seconds;
            this.TriggerType = TriggerType.simple;
        }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 任务描述
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 上次执行时间
        /// </summary>
        public DateTime? LastTime { get; set; }
        /// <summary>
        /// 任务执行次数
        /// </summary>
        public int ExecuteCount { get; set; }
        /// <summary>
        /// 任务开始时间,可以为空，为空就是当前时间立即启动
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间，结束时间为空则是永久执行
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 程序集名称
        /// </summary>
        public string AssemblyName { get; set; }
        /// <summary>
        /// 类型的名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public JobStatus Status { get; set; }
        /// <summary>
        /// 触发器类型
        /// </summary>
        public TriggerType TriggerType { get; set; }
        /// <summary>
        /// Cron表达式
        /// </summary>
        public string CronExpression { get; set; }
        /// <summary>
        /// 触发器时间间隔
        /// </summary>
        public TriggerInterval TriggerInterval  { get; set; }
        /// <summary>
        /// 间隔
        /// </summary>
        public int Intervals { get; set; }


        /// <summary>
        /// Job的唯一值
        /// </summary>
        [NotMapped]
        public string JobKey
        {
            get
            {
                return Id.ToString();
            }
        }
        /// <summary>
        /// Job的组别
        /// </summary>
        [NotMapped]
        public string JobGroup
        {
            get
            {
                return "group" + Id;
            }
        }
        /// <summary>
        /// 触发器的标识
        /// </summary>
        [NotMapped]
        public string TriggerKey
        {
            get
            {
                return "trigger" + Id;
            }
        }

    }
}
