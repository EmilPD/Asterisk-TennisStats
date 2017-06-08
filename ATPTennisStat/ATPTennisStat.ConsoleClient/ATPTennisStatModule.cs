using Ninject;
using Ninject.Modules;
using System;
using ATPTennisStat.SQLServerData;
using System.Data.Entity;
using ATPTennisStat.Repositories.Contracts;
using ATPTennisStat.Repositories;
using ATPTennisStat.PostgreSqlData;
using ATPTennisStat.Common;

namespace ATPTennisStat.ConsoleClient
{
    public class ATPTennisStatModules : NinjectModule
    {
        public override void Load()
        {
            // TODO: Resolve duplicate DBContexts in Kernel???
            //this.Bind<DbContext>().To<SqlServerDbContext>().InSingletonScope().Named(Constants.SqlDbContextName);
            this.Bind<DbContext>().To<PostgresDbContext>().InSingletonScope().Named(Constants.PostgreDbContextName);
            this.Bind(typeof(IRepository<>)).To(typeof(EfRepository<>));
            this.Bind<Func<IUnitOfWork>>().ToMethod(ctx => () => ctx.Kernel.Get<EfUnitOfWork>());
            this.Bind<IUnitOfWork>().To<EfUnitOfWork>();
        }
    }
}
