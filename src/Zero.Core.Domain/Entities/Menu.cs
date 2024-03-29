﻿using System;
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
    /// 菜单表
    /// </summary>
    public class Menu:Entity
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

        public ICollection<RoleMenu> RoleMenus { get; set; }
        /// <summary>
        /// 集合导航属性
        /// </summary>
        public List<Permission>  Permissions { get; set; }

    }
}
