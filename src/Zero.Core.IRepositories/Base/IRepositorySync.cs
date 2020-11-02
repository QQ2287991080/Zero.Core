using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities.Base;

namespace Zero.Core.IRepositories.Base
{
    public partial interface IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns>返回所有数据</returns>
        List<TEntity> GetAll();
        /// <summary>
        /// 根据条件获取全部数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回所有数据</returns>
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据条件以及排序获取数据
        /// </summary>
        /// <typeparam name="TProperty">排序字段</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="isAsc">是否正序（默认倒叙）</param>
        /// <returns>返回所有数据</returns>
        List<TEntity> GetAll<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> order, bool isAsc = false);

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <typeparam name="TProperty">排序字段</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="isAsc">是否正序（默认倒叙）</param>
        /// <returns>返回数据总数量和数据</returns>
        Tuple<int, List<TEntity>> GetPage<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> order, int pageIndex, int pageSize, bool isAsc = false);
        /// <summary>
        /// 根据主键id获取实体数据
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns>返回实体或空</returns>
        TEntity First(int id);
        /// <summary>
        /// 根据查询条件获取实体数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回实体或空</returns>
        TEntity First(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据条件判断数据是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Any(Expression<Func<TEntity, bool>> predicate);
    }
}
