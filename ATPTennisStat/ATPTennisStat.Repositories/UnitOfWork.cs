using ATPTennisStat.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
