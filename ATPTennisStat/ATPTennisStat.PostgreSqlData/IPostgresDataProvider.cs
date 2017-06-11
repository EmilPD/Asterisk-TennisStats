using ATPTennisStat.Models.PostgreSqlModels;
using ATPTennisStat.Repositories.Contracts;

namespace ATPTennisStat.PostgreSqlData
{
    public interface IPostgresDataProvider
    {
        IRepository<TennisEvent> TennisEvents { get; set; }

        IRepository<Ticket> Tickets { get; set; }

        IUnitOfWork UnitOfWork { get; set; }
    }
}