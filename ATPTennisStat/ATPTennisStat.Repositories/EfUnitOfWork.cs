using System.Data.Entity;
using ATPTennisStat.Repositories.Contracts;

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
    }
}