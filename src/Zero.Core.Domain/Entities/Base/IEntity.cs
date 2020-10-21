using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.Domain.Entities.Base
{
    /// <summary>
    /// 定义接口base字段便于操作字段
    /// </summary>
    public interface IEntity
    {
        int Id { get; set; }
        DateTime CreateTime { get; set; }

        DateTime? ModifyTime { get; set; }

        DateTime? DeleteTime { get; set; }

        bool IsDelete { get; set; }
    }

    /// <summary>
    /// 实现接口IEntity,所有实体继承该这个基类
    /// </summary>
    public abstract class Entity : IEntity
    {
        public Entity()
        {
            CreateTime = DateTime.Now;
            IsDelete = false;
        }
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }

        public DateTime? ModifyTime { get; set; }

        public DateTime? DeleteTime { get; set; }

        public bool IsDelete { get; set; }

    }
}
