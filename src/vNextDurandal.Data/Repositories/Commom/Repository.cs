using System;
using System.Linq;
using System.Linq.Expressions;
using vNextDurandal.Commom.Abstract;
using vNextDurandal.Commom.Interfaces.Repository;
using vNextDurandal.Commom.Interfaces.UoW;

namespace vNextDurandal.Data.Repositories.Commom
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly IUnitOfWork _uow;
        public virtual Expression<Func<TEntity, bool>> DefaultFilter => (t) => true;

        public Repository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public virtual void SetDefaultValues(TEntity entity) { }

        public IQueryable<TEntity> List() => _uow.Context.Set<TEntity>().Where(DefaultFilter);

        public TEntity Find(long id) => List().FirstOrDefault(e => e.Id == id);

        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> filter) => List().Where(filter);

        public TEntity Include(TEntity entity)
        {
            SetDefaultValues(entity);
            _uow.Context.Set<TEntity>().Add(entity);
            _uow.Context.SaveChanges();
            return entity;
        }

        public void Delete(long id) => Delete(Find(id));

        public void Delete(TEntity entity)
        {
            _uow.Context.Set<TEntity>().Remove(entity);
            _uow.SaveChanges();
        }

        public TEntity Update(TEntity entity)
        {
            _uow.Context.Set<TEntity>().Update(entity);
            _uow.SaveChanges();
            return entity;
        }
    }
}
