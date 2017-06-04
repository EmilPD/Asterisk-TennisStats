using System.Data.Entity;
using ATPTennisStat.Models;

namespace ATPTennisStat.SQLServerData
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext()
            : base("name=ATPTennisStatSqlServer14")
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

        }
    }
}
