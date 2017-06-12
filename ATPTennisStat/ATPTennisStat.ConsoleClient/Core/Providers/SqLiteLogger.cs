using System;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Models.SqliteModels;
using ATPTennisStat.SQLiteData;

namespace ATPTennisStat.ConsoleClient.Core.Providers
{
    public class SqLiteLogger : ILogger
    {
        private readonly ISqliteDataProvider provider;

        public SqLiteLogger(ISqliteDataProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("SqliteDataProvider");
            }
    
            this.provider = provider;
        }

        public void Log(string message)
        {
            this.provider.Logs.Add(new Log { Message = message, TimeStamp = DateTime.Now });
            this.provider.UnitOfWork.Finished();
        }
    }
}