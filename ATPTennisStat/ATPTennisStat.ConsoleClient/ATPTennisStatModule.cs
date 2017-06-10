using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Modules;
using System;
using ATPTennisStat.SQLServerData;
using System.Data.Entity;
using System.IO;
using System.Reflection;
using ATPTennisStat.Repositories.Contracts;
using ATPTennisStat.Repositories;
using ATPTennisStat.PostgreSqlData;
using ATPTennisStat.Common.Enums;
using ATPTennisStat.ConsoleClient.Core;
using ATPTennisStat.ConsoleClient.Core.Commands.TicketCommands;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.ConsoleClient.Core.Factories;
using ATPTennisStat.ConsoleClient.Core.Providers;
using ATPTennisStat.Common;

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

            this.Kernel.Bind(x =>
            {
                x.FromAssembliesInPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                    .SelectAllClasses()
                    .Where(type => type != typeof(Engine))
                    .BindDefaultInterface();
            });

            Bind<IEngine>().To<Engine>().InSingletonScope();

            var buytickets = Bind<BuyTicketsCommand>().ToSelf().InSingletonScope();
            var showtickets = Bind<ShowTicketsCommand>().ToSelf().InSingletonScope();
            buytickets.WithConstructorArgument(this.Kernel.Get<DbContext>());

            Bind<IReader>().To<ConsoleReader>().InSingletonScope();
            Bind<IWriter>().To<ConsoleWriter>().InSingletonScope();
            Bind<IParser>().To<CommandParser>().InSingletonScope();
            Bind<ILogger>().To<SqLiteLogger>().InSingletonScope();

            Bind<ICommandFactory>().To<CommandFactory>().InSingletonScope();
        }
    }
}
