using System.Linq.Expressions;
using MvcIBF.Models;

namespace MvcIBF.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        void Add(T entity);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
        void Remove(T entity); 
        void RemoveRange(IEnumerable<T> entities);
    }
}
