using System.Data.Entity;
using ATPTennisStat.Repositories.Contracts;
using System.Data.Entity.Infrastructure;

namespace ATPTennisStat.Repositories
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private DbContext context;

        public EfUnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public void Finished()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {
        }

        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return context.Entry<TEntity>(entity);
        }
    }
}