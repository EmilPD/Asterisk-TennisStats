using System.Data.Entity;
using Ninject;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.PostgreSqlData;
using ATPTennisStat.SQLiteData;
using ATPTennisStat.SQLServerData;

namespace ATPTennisStat.ConsoleClient
{
    class Startup
    {
        static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SqlServerDbContext, SQLServerData.Migrations.Configuration>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PostgresDbContext, PostgreSqlData.Migrations.Configuration>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SqliteDbContext, SQLiteData.Migrations.Configuration>(true));

            var kernel = new StandardKernel(new ATPTennisStatModules());
            var engine = kernel.Get<IEngine>();
            engine.Start();
        }
    }
}