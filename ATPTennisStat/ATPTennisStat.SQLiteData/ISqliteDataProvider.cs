using ATPTennisStat.Models.SqliteModels;
using ATPTennisStat.Repositories.Contracts;

namespace ATPTennisStat.SQLiteData
{
    public interface ISqliteDataProvider
    {
        IRepository<Log> Logs { get; set; }

        IUnitOfWork UnitOfWork { get; set; }
    }
}