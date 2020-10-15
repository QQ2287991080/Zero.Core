using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Zero.Core.Domain.Entities.Base;
using Zero.Core.IRepositories.Base;
using Zero.Core.IServices.Base;

namespace Zero.Core.Services.Base
{
    public class BaseService<TEntity>:IBaseService<TEntity> where TEntity : class, IEntity, new()
    {
        protected IRepository<TEntity> _repository;
        public BaseService()
        {
          
        }
        public IQueryable<TEntity> Query()
        {
            return _repository.Query();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.GetAllAsync(predicate);
        }
        public async Task<List<TEntity>> GetAllAsync<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> order, bool isAsc = false)
        {
            return await _repository.GetAllAsync(predicate, order, isAsc);
        }

        public async Task<Tuple<int, List<TEntity>>> GetPageAsync<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> order, int pageIndex, int pageSize, bool isAsc = false)
        {
            return await _repository.GetPageAsync(predicate, order, pageIndex, pageSize, isAsc);
        }

        public async Task<TEntity> FirstAsync(int id)
        {
            return await _repository.FirstAsync(id);
        }

        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.FirstAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.AnyAsync(predicate);
        }


        public async Task<TEntity> AddAsync(TEntity entity)
        {
            return await _repository.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _repository.AddRangeAsync(entities);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            await _repository.UpdateRangeAsync(entities);
        }


        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
        public async Task DeleteAsync(TEntity entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            await _repository.DeleteRangeAsync(entities);
        }

        public async Task SaveAsync()
        {
            await _repository.SaveAsync();
        }
    }
}
