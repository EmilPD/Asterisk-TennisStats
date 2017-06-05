using System.Data.Entity;
using ATPTennisStat.Models;
using ATPTennisStat.SQLServerData.EntityConfigurations;

namespace ATPTennisStat.SQLServerData
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext()
            : base("ATPTennisStatSqlServer")
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

        public virtual IDbSet<Match> Matches { get; set; }

        public virtual IDbSet<Round> Rounds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<City>()
            //    .HasMany(c => c.Players);

            modelBuilder.Configurations.Add(new CityConfiguration());
            modelBuilder.Configurations.Add(new CoachConfiguration());
            modelBuilder.Configurations.Add(new PlayerConfiguration());
            modelBuilder.Configurations.Add(new TournamentCategoryConfiguration());
            modelBuilder.Configurations.Add(new SurfaceConfiguration());
            modelBuilder.Configurations.Add(new UmpireConfiguration());
            modelBuilder.Configurations.Add(new MatchConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
