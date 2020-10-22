using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.Common.Extensions
{
    public static class EnumExtension
    {
        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static object GetValue(Enum anyenum)
        {
            var type = Enum.GetUnderlyingType(anyenum.GetType());
            return Convert.ChangeType(anyenum, type);
        }

        /// <summary>
        /// 获取枚举的Description特性的描述信息，如果没有特性返回枚举的名称
        /// </summary>
        /// <param name="anyenum"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum anyenum)
        {
            var type = anyenum.GetType();//获取枚举的类型
            var name = Enum.GetName(type, anyenum);//获取枚举值的名字
            if (name == null) return null;
            var filed = type.GetField(name);//查看枚举类型中是否有这个枚举值
            var attribute = Attribute.GetCustomAttribute(filed, typeof(DescriptionAttribute)) as DescriptionAttribute;//获取备注特性
            if (attribute == null) return name;
            return attribute?.Description;
        }
    }
}
