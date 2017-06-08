using Ninject;
using Ninject.Modules;
using System;
using ATPTennisStat.SQLServerData;
using System.Data.Entity;
using ATPTennisStat.Repositories.Contracts;
using ATPTennisStat.Repositories;
using ATPTennisStat.PostgreSqlData;
using ATPTennisStat.Common;
using ATPTennisStat.Common.Enums;

namespace ATPTennisStat.ConsoleClient
{
    public class ATPTennisStatModules : NinjectModule
    {
        private readonly DbContextType contextType;

        public ATPTennisStatModules(DbContextType contextType)
        {
            this.contextType = contextType;
        }
        public override void Load()
        {
            if (this.contextType == DbContextType.Postgre)
            {
                this.Bind<DbContext>().To<PostgresDbContext>().InSingletonScope();
            }
            else if (this.contextType == DbContextType.SQLServer)
            {
                this.Bind<DbContext>().To<SqlServerDbContext>().InSingletonScope();
            }
            else
            {
                //this.Bind<DbContext>().To<SqliteDbContext>().InSingletonScope();
            }

            this.Bind(typeof(IRepository<>)).To(typeof(EfRepository<>));
            this.Bind<Func<IUnitOfWork>>().ToMethod(ctx => () => ctx.Kernel.Get<EfUnitOfWork>());
            this.Bind<IUnitOfWork>().To<EfUnitOfWork>();
        }
    }
}
