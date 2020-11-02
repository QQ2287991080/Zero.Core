using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities;
using Zero.Core.IRepositories.Base;

namespace Zero.Core.IRepositories
{
    public interface IDictionariesRepository:IRepository<Dictionaries>
    {
        /// <summary>
        /// 根据id获取名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> GetNameAsync(int? id);
        /// <summary>
        /// 根据id集合返回按给点字符拼接的字符串
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        Task<string> GetNamesAsync(IEnumerable<int> ids, string separator = ",");
        /// <summary>
        /// 根据名称获取id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<int?> GetIdByNameAsync(string name);
        /// <summary>
        /// 根据名称获取id集合
        /// </summary>
        /// <param name="names"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        Task<IEnumerable<int>> GetIdsByNamesAsync(IEnumerable<string> names);
        /// <summary>
        /// 根据id获取名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetName(int? id);
        /// <summary>
        /// 根据id集合返回按给点字符拼接的字符串
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        string GetNames(IEnumerable<int> ids, string separator = ",");
        /// <summary>
        /// 根据名称获取id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        int? GetIdByName(string name);
        /// <summary>
        /// 根据名称获取id集合
        /// </summary>
        /// <param name="names"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        IEnumerable<int> GetIdsByNames(IEnumerable<string> names, string separator = ",");

    }
}
 