using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.Domain.Dtos
{
    /// <summary>
    /// 分页返回数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListResult<T>
    {
        public ListResult(int pageIndex,int pageSize,int count, IEnumerable<T> data)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.Count = count;
            this.Data = data;
        }


        public ListResult(int pageIndex, int pageSize, IEnumerable<T> data)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.Count = data.Count();
            this.Data = data.Skip((pageIndex-1) * pageSize).Take(pageSize);
        }
        public ListResult() : this(0, 0, 0,new List<T>())
        {
        }
        /// <summary>
        /// 数据总数
        /// </summary>
        public int Count { get; }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; }
        /// <summary>
        /// 页数
        /// </summary>
        public int PageSize { get; }
        /// <summary>
        /// 数据
        /// </summary>
        public IEnumerable<T> Data { get;}
    }
}
