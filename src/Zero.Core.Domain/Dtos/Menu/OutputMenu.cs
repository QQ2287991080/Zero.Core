using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities.Base;
using Zero.Core.Domain.Enums;

namespace Zero.Core.Domain.Dtos.Menu
{

    public class OutputMenu:Entity
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 路由
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 图标类型
        /// </summary>
        public IconType? IconType { get; set; }
        /// <summary>
        /// 样式
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsAllow { get; set; }
        /// <summary>
        /// 父级菜单
        /// </summary>
        public int? IdParent { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public List<OutputMenu> Childrens { get; set; }

        public static void Resolver(IEnumerable<OutputMenu> treeNodes, OutputMenu model)
        {
            var nodes = treeNodes.Where(p => p.IdParent == model.Id);
            if (nodes == null || nodes.Count() == 0)
            {
                model.Childrens = null;
            }
            model.Childrens = new List<OutputMenu>();
            foreach (var item in nodes.OrderBy(o => o.Sort))
            {
                model.Childrens.Add(item);
                Resolver(treeNodes, item);
            }
        }
    }
}
