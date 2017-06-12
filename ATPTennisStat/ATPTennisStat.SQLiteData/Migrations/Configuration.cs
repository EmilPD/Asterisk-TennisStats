using System.Data.SQLite.EF6.Migrations;
using System.Data.Entity.Migrations;

namespace ATPTennisStat.SQLiteData.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<ATPTennisStat.SQLiteData.SqliteDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            SetSqlGenerator("System.Data.SQLite", new SQLiteMigrationSqlGenerator());
        }

        protected override void Seed(ATPTennisStat.SQLiteData.SqliteDbContext context)
        {
        }
    }
}