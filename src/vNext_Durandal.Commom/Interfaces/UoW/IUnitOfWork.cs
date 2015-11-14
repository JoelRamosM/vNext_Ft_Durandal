using System;
using System.Data.Entity;

namespace vNext_Durandal.Commom.Interfaces.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        DbContextTransaction Transaction { get; }
        DbContext Context { get; }
        void Start();
        void Save();
        void SaveChanges();
        void Rollback();
    }
}
