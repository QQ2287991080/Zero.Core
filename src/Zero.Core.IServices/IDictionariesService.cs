using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Dtos.Dictionaries;
using Zero.Core.Domain.Entities;
using Zero.Core.IServices.Base;

namespace Zero.Core.IServices
{
    public interface IDictionariesService:IBaseService<Dictionaries>, ISupportService
    {
        /// <summary>
        /// 名称是否存在，这个名称是允许重复的，但是在同一父级的情况下是不允许重复的
        /// </summary>
        /// <param name="name"></param>
        /// <param name="idParent"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> NameContains(string name, int? idParent, int id = 0);
        /// <summary>
        /// 获取字典树
        /// </summary>
        /// <returns></returns>
        Task<List<OutputDicDto>> GetTree();
    }
}
