using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.xUnitTest.Attributes
{
    /// <summary>
    /// 自定义排序显示特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method| AttributeTargets.Class, AllowMultiple = false)]
    public class OrderAttribute : Attribute
    {
        public OrderAttribute(int order = 0)
        {
            Order = order;
        }
        public int Order { get; }
    }
}
