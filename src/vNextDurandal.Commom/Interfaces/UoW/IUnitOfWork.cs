using System;
using Microsoft.Data.Entity;

namespace vNextDurandal.Commom.Interfaces.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }
        void Start();
        void Save();
        void SaveChanges();
        void Rollback();
    }
}
