using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.Domain.Dtos
{
    /// <summary>
    /// 分页
    /// </summary>
    public abstract class PageCondition
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 页数
        /// </summary>
        public int PageSize { get; set; } = 20;
    }
}
