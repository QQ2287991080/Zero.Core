using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.Common.Helper
{
    /// <summary>
    /// 随机数生成帮助类
    /// </summary>
    public class RandomHelper
    {
        static Random random ;
        static readonly object _randomLock = new object();
        public RandomHelper()
        {
            random = new Random();
        }
        /// <summary>
        /// 获取六位数随机数
        /// </summary>
        /// <returns></returns>
        public static int GetRandomNumber()
        {
            lock (_randomLock)
            {
                int number = random.Next(100000, 999999);
                return number;
            }
        }
        /// <summary>
        /// 根据数字区间获取随机数
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns></returns>
        public static int GetRandomNumber(int minValue,int maxValue)
        {
            lock (_randomLock)
            {
                int number = random.Next(minValue, maxValue);
                return number;
            }
        }

        /// <summary>
        /// 获取随机数
        /// </summary>
        /// <returns></returns>
        public static int GetRandomNumberCore()
        {
            Random random = new Random();
            return random.Next(100000, 999999);
        }
    }
}
