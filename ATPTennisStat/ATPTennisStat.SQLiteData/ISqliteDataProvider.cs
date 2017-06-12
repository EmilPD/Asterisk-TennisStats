using ATPTennisStat.Models.SqliteModels;
using ATPTennisStat.Repositories.Contracts;

namespace ATPTennisStat.SQLiteData
{
    public interface ISqliteDataProvider
    {
        IRepository<Log> Logs { get; set; }

        IRepository<LogDetail> LogDetails { get; set; }

        IUnitOfWork UnitOfWork { get; set; }
    }
}