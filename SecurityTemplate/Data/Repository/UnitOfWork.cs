using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Security.Core.Model;
using Security.Core.Repository;
using Security.Data;
using Security.Data.Factories;

namespace Security.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContextFactory dbContextFactory;
        private DbContext dataContext;

        public UnitOfWork(IDbContextFactory databaseFactory)
        {
            this.dbContextFactory = databaseFactory;
        }

        protected DbContext DataContext
        {
            get { return dataContext ?? (dataContext = dbContextFactory.GetContext()); }
        }

        public void Commit()
        {
            DataContext.SaveChanges();
        }
    }
}
