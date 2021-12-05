namespace Hospital.DataAccess.Repositories.Abstraction
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using NHibernate;

    public interface IRepository<TEntity>
    {
        TEntity Get(ISession session, int id);

        TEntity Find(ISession session, Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetAll(ISession session);

        IQueryable<TEntity> Filter(ISession session, Expression<Func<TEntity, bool>> predicate);
    }
}
