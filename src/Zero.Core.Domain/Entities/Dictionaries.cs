using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities.Base;

namespace Zero.Core.Domain.Entities
{
    /// <summary>
    /// 可维护类型表
    /// </summary>
    public class Dictionaries:Entity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 父级id
        /// </summary>
        public int? IdParent { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsAllow { get; set; }
    }
}
