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
}
