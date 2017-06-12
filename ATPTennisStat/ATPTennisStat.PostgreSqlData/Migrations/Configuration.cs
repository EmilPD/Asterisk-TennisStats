using System.Data.Entity.Migrations;

namespace ATPTennisStat.PostgreSqlData.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<ATPTennisStat.PostgreSqlData.PostgresDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ATPTennisStat.PostgreSqlData.PostgresDbContext context)
        {
        }
    }
}