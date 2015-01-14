using System;
using System.Linq;
using System.Linq.Expressions;

namespace Security.Core.Repository
{
    public interface IRepo<T> : IDisposable
    {
        T Get(int id);
        IQueryable<T> GetAll();
        T Add(T o);
        void Save();
        void Update(T o);
        void Delete(T o);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate, bool showDeleted = false);
        int Count();
    }
}
