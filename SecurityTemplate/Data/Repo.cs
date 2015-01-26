using System;
using System.Linq;
using System.Linq.Expressions;
using Security.Core.Repository;
using System.Data.Entity;
using Omu.ValueInjecter;

namespace Security.Data
{
    public class Repo<T> : IRepo<T> where T : class//, new()
    {
        protected readonly DbContext dbContext;
        
        //protected readonly RepoHelper<T> repoHelper = new RepoHelper<T>();

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

        public virtual void Update(T o)//, params String[] pkey)
        {
            dbContext.Entry(o).State = EntityState.Modified;
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
