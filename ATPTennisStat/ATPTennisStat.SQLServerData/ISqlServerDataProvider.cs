using ATPTennisStat.Models;
using ATPTennisStat.Repositories.Contracts;

namespace ATPTennisStat.SQLServerData
{
    public interface ISqlServerDataProvider
    {
        IRepository<City> Cities { get; set; }

        IRepository<Country> Countries { get; set; }

        IRepository<Match> Matches { get; set; }

        IRepository<Player> Players { get; set; }

        IRepository<PointDistribution> PointDistributions { get; set; }

        IRepository<Round> Rounds { get; set; }

        IRepository<Surface> Surfaces { get; set; }

        IRepository<TournamentCategory> TournamentCategories { get; set; }

        IRepository<Tournament> Tournaments { get; set; }

        IUnitOfWork UnitOfWork { get; set; }
    }
}