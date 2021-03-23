using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities.Base;

namespace Zero.Core.Domain.Entities
{
    /// <summary>
    /// 轮询图表
    /// </summary>
    public class PhotoManager: Entity
    {
        /// <summary>
        /// 图片标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 图片样式
        /// </summary>
        public string PhotoClass { get; set; }
        /// <summary>
        /// 页面跳转链接
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

    }
}
