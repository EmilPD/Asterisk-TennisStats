using ATPTennisStat.Models;
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
            this.Cities = new EfRepository<City>(context);
        }

        public IRepository<City> Cities { get; private set; }

        public void Finished()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}