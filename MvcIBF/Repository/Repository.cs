using Microsoft.EntityFrameworkCore;
using MvcIBF.Data;
using MvcIBF.Repository.IRepository;
using System.Linq.Expressions;

namespace MvcIBF.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MvcIBFContext _db;
        internal DbSet<T> dbSet;
        public Repository(MvcIBFContext db)
        {
            _db = db;
            this.dbSet=_db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query=query.Where(filter);
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
