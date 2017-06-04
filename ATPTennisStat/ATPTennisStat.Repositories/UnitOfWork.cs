

using ATPTennisStat.Repositories.Contracts;

namespace ATPTennisStat.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ATPTennisStatContext context;

        public UnitOfWork(ATPTennisStatContext context)
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
