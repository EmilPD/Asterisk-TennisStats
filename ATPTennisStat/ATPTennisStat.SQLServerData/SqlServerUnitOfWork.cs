using ATPTennisStat.Repositories.Contracts;
using ATPTennisStat.SQLServerData;

namespace ATPTennisStat.Repositories
{
    public class SqlServerUnitOfWork : IUnitOfWork
    {
        private readonly SqlServerDbContext context;

        public SqlServerUnitOfWork(SqlServerDbContext context)
        {
            this.context = context;
        }

        public int Finished()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}