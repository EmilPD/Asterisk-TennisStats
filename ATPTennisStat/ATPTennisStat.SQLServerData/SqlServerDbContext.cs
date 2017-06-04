using System.Data.Entity;
using ATPTennisStat.Models;
using ATPTennisStat.SQLServerData.EntityConfigurations;

namespace ATPTennisStat.SQLServerData
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext()
            : base("ATPTennisStatSqlServer14")
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual IDbSet<City> Cities { get; set; }

        public virtual IDbSet<Country> Countries { get; set; }

        public virtual IDbSet<Coach> Coaches { get; set; }

        public virtual IDbSet<Player> Players { get; set; }

        public virtual IDbSet<Surface> Surfaces { get; set; }

        public virtual IDbSet<Tournament> Tournaments { get; set; }

        public virtual IDbSet<TournamentCategory> TournamentCategories { get; set; }

        public virtual IDbSet<Umpire> Umpires { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<City>()
            //    .HasMany(c => c.Players);

            modelBuilder.Configurations.Add(new CityConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
