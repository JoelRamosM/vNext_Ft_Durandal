using System.Data.Entity;
using vNext_Durandal.Commom.Interfaces.UoW;

namespace vNext_Durandal.Data.Configuration
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _rolledBack;
        public DbContextTransaction Transaction { get; private set; }
        public DbContext Context { get; }

        public UnitOfWork()
        {
            Context = new VNextContext();
            Start();
        }

        public void Start()
        {
            Transaction = Transaction ?? Context.Database.BeginTransaction();
        }

        public void Save()
        {
            Transaction.Commit();
            Context.SaveChanges();
        }

        public void SaveChanges() => Context.SaveChanges();

        public void Rollback()
        {
            Transaction.Rollback();
            _rolledBack = true;
        }

        public void Dispose()
        {
            if (!_rolledBack)
                Save();
            Context.Dispose();
        }
    }
}
