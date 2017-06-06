using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATPTennisStat.SQLServerData;
using System.Data.Entity;
using ATPTennisStat.Repositories.Contracts;
using ATPTennisStat.Repositories;

namespace ATPTennisStat.ConsoleClient
{
    public class ATPTennisStatModules : NinjectModule
    {
        public override void Load()
        {
            this.Bind<DbContext>().To<SqlServerDbContext>().InSingletonScope();
            this.Bind(typeof(IRepository<>)).To(typeof(EfRepository<>));
            //this.Bind<Func<IUnitOfWork>>().ToMethod(ctx => () => ctx.Kernel.Get<EfUnitOfWork>());
            this.Bind<IUnitOfWork>().To<EfUnitOfWork>();
        }
    }
}
