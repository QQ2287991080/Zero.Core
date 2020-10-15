using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Core.EfCore;

namespace Zero.Core.IRepositories.Base
{
    public interface IUnitOfWork
    {
        EfCoreDbContext DbContext { get; }
        #region Nomal
        void Begin();
        void Commit();
        void Rollback();
        void Save();
        #endregion

        #region Asynchronous
        Task BeginAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task SaveAsync();
        #endregion
    }
}
