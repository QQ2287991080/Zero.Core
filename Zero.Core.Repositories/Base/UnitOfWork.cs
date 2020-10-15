using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;
using Zero.Core.EfCore;
using Zero.Core.IRepositories.Base;

namespace Zero.Core.Repositories.Base
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly EfCoreDbContext _dbContext;
        private IDbContextTransaction _transaction;
        public UnitOfWork(EfCoreDbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException("ef core dbcontext can not as null");
        }

        public EfCoreDbContext DbContext
        {
            get
            {
                return _dbContext;
            }
        }

        public void Begin()
        {
            if (_transaction == null)
            {
                _transaction = DbContext.Database.BeginTransaction();
            }
        }

        public void Commit()
        {
            if (_transaction != null)
            {
                Save();
                _transaction.Commit();
                Disposable();
            }
        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                Disposable();
            }
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        private void Disposable()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
        public async Task BeginAsync()
        {
            if (_transaction == null)
            {
                await DbContext.Database.BeginTransactionAsync();
            }
        }
        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await SaveAsync();
                await _transaction.CommitAsync();
                await DisposableAsync();
            }
        }
        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await DisposableAsync();
            }
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        private async Task DisposableAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}
