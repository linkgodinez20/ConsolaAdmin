using System;
using System.Linq;
using System.Linq.Expressions;
using Security.Core.Repository;
using System.Data.Entity;
using Omu.ValueInjecter;
using Security.Data.Factories;

namespace Security.Data.Repository
{
    public class Repo<T> : IRepo<T> where T : class
    {
        protected readonly DbContext dbContext;

        public Repo(IDbContextFactory f)
        {
            dbContext = f.GetContext();
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public T Add(T o)
        {
            var t = dbContext.Set<T>().Create();
            t.InjectFrom(o);
            dbContext.Set<T>().Add(t);
            return t;
        }

        public virtual void Delete(T o)
        {
            dbContext.Set<T>().Remove(o);
        }

        public virtual void Update(T o)
        {
            dbContext.Entry(o).State = EntityState.Modified;
        }

        public virtual void Refresh(T o)
        {
            dbContext.Entry(o).GetDatabaseValues();
        }

        public T Get(params Object[] KeyValues)
        {
            return dbContext.Set<T>().Find(KeyValues);
        }

        public virtual IQueryable<T> Where(Expression<Func<T, bool>> predicate, bool showDeleted = false)
        {
            return dbContext.Set<T>().Where(predicate);
        }

        public virtual IQueryable<T> GetAll()
        {
            return dbContext.Set<T>();            
        }

        public virtual int Count()
        {
            return dbContext.Set<T>().Count();
        }

        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            return dbContext.Set<T>().Count(predicate);
        }

        public virtual bool ProxyCreationEnabled(bool val)
        {
            return dbContext.Configuration.ProxyCreationEnabled = val;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }

}
