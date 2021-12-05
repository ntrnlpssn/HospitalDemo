namespace Hospital.DataAccess.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Hospital.DataAccess.Repositories.Abstraction;
    using Hospital.Domain;
    using NHibernate;

    public class ChamberRepository : IRepository<Chamber>
    {
        public Chamber Get(ISession session, int id) => session?.Get<Chamber>(id);

        public Chamber Find(ISession session, Expression<Func<Chamber, bool>> predicate)
        {
            return this.GetAll(session).FirstOrDefault(predicate);
        }

        public IQueryable<Chamber> GetAll(ISession session) => session?.Query<Chamber>();

        public IQueryable<Chamber> Filter(ISession session, Expression<Func<Chamber, bool>> predicate)
        {
            return this.GetAll(session).Where(predicate);
        }
    }
}
