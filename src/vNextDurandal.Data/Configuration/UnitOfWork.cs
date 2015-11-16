using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Storage;
using vNextDurandal.Commom.Interfaces.UoW;

namespace vNextDurandal.Data.Configuration
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _rolledBack;
        private IRelationalTransaction transaction;
        private bool _isDisposed;

        public DbContext Context { get; }

        public UnitOfWork(VNextContext context)
        {
            Context = context;
            Start();
        }

        public void Start()
            => transaction = transaction ?? Context.Database.BeginTransaction();

        public void Save()
        {
            if (_isDisposed) return;
            transaction.Commit();
            Context.SaveChanges();
        }

        public void SaveChanges() => Context.SaveChanges();

        public void Rollback()
        {
            Context.Database.RollbackTransaction();
            _rolledBack = true;
        }

        public void Dispose()
        {
            _isDisposed = true;
            if (!_rolledBack)
                Save();
            Context.Dispose();
        }
    }
}
