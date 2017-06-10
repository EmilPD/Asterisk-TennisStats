using System.IO;
using System.Reflection;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Modules;
using ATPTennisStat.ConsoleClient.Core;
using ATPTennisStat.ConsoleClient.Core.Commands.TicketCommands;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.ConsoleClient.Core.Factories;
using ATPTennisStat.ConsoleClient.Core.Providers;
using ATPTennisStat.SQLServerData;
using ATPTennisStat.PostgreSqlData;
using ATPTennisStat.Repositories.Contracts;
using ATPTennisStat.Repositories;

namespace ATPTennisStat.ConsoleClient
{
    public class ATPTennisStatModules : NinjectModule
    {
        public override void Load()
        {
            this.Bind<SqlServerDbContext>().ToSelf().InSingletonScope().Named("SqlServer");
            this.Bind<PostgresDbContext>().ToSelf().InSingletonScope().Named("Postgre");

            this.Bind<IUnitOfWork>().To<EfUnitOfWork>()
                .WhenInjectedInto<SqlServerDataProvider>()
                .WithConstructorArgument("context", Kernel.Get<SqlServerDbContext>("SqlServer"));

            this.Bind(typeof(IRepository<>)).To(typeof(EfRepository<>))
                .WhenInjectedInto<SqlServerDataProvider>()
                .WithConstructorArgument("context", Kernel.Get<SqlServerDbContext>("SqlServer"));

            this.Bind<IUnitOfWork>().To<EfUnitOfWork>()
                .WhenInjectedInto<PostgresDataProvider>()
                .WithConstructorArgument("context", Kernel.Get<PostgresDbContext>("Postgre"));

            this.Bind(typeof(IRepository<>)).To(typeof(EfRepository<>))
                .WhenInjectedInto<PostgresDataProvider>()
                .WithConstructorArgument("context", Kernel.Get<PostgresDbContext>("Postgre"));

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
            buytickets.WithConstructorArgument(this.Kernel.Get<PostgresDbContext>());

            Bind<IReader>().To<ConsoleReader>().InSingletonScope();
            Bind<IWriter>().To<ConsoleWriter>().InSingletonScope();
            Bind<IParser>().To<CommandParser>().InSingletonScope();
            Bind<ILogger>().To<SqLiteLogger>().InSingletonScope();

            Bind<ICommandFactory>().To<CommandFactory>().InSingletonScope();
        }
    }
}