using ATPTennisTickets.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisTickets.Data
{
    public class PostgresDbContext : DbContext
    {
        public PostgresDbContext() : base("PostgresDotNet")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

            //Helpful for debugging            
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<PostgresDbContext>(null);

            modelBuilder.HasDefaultSchema("public");
            // modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

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
