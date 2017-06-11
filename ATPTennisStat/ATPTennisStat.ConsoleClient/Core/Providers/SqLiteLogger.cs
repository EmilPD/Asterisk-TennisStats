using System;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Models.SqliteModels;
using ATPTennisStat.SQLiteData;

namespace ATPTennisStat.ConsoleClient.Core.Providers
{
    public class SqLiteLogger : ILogger
    {
        private readonly SqliteDataProvider provider;
        private readonly IWriter writer;

        public SqLiteLogger(SqliteDataProvider provider, IWriter writer)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("SqliteDataProvider");
            }

            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            this.provider = provider;
            this.writer = writer;
        }

        public void Log(string message)
        {
            this.writer.WriteLine($"{message}!");
            this.provider.Logs.Add(new Log { Message = message, TimeStamp = DateTime.Now });
            this.provider.UnitOfWork.Finished();
        }
    }
}