using System.Data.Entity.Migrations;

namespace ATPTennisStat.SQLServerData.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<SqlServerDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SqlServerDbContext context)
        {
        }
    }
}