using System;
using System.Linq;
using System.Linq.Expressions;
using vNextDurandal.Commom.Abstract;

namespace vNextDurandal.Commom.Interfaces.Repository
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {

        Expression<Func<TEntity, bool>> DefaultFilter { get; }

        void SetDefaultValues(TEntity entity);

        TEntity Find(long id);

        IQueryable<TEntity> List();

        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> filter);

        TEntity Include(TEntity entity);

        void Delete(long id);

        void Delete(TEntity entity);

        TEntity Update(TEntity entity);
    }
}
