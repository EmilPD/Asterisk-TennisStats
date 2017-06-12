using System.Data.Entity;
using ATPTennisStat.SQLiteData.EntityConfigurations;
using ATPTennisStat.SQLiteData.Migrations;
using ATPTennisStat.Models.SqliteModels;

namespace ATPTennisStat.SQLiteData
{
    public class SqliteDbContext : DbContext
    {
        public SqliteDbContext() : base("ATPTennisStatSQLite")
        {
        }

        public virtual IDbSet<Log> Logs { get; set; }

        public virtual IDbSet<LogDetail> LogDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LogConfigurations());

            base.OnModelCreating(modelBuilder);
        }
    }
}