using ATPTennisStat.Models.PostgreSqlModels;
using System.Data.Entity;

namespace ATPTennisStat.PostgreSqlData
{
    public class PostgresDbContext : DbContext
    {
        public PostgresDbContext() : base("ATPTennisTickets")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;         
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public virtual IDbSet<Ticket> Tickets { get; set; }

        public virtual IDbSet<TennisEvent> TennisEvents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Properties().Configure(c =>
            {
                var name = c.ClrPropertyInfo.Name;
                var newName = name.ToLower();
                c.HasColumnName(newName);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
