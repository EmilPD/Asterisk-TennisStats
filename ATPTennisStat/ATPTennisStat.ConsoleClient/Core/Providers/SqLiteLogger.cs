using System;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Models.SqliteModels;
using ATPTennisStat.SQLiteData;
using System.Collections.Generic;

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
        public Log CreateNewLog(string command)
        {
            var newLog =new Log
            {
                Message = command
            };

            this.provider.Logs.Add(newLog);
            return newLog;
        }

        public LogDetail CreateNewLogDetail(string message, Log log)
        {
            var newlogDetail = new LogDetail
            {
                Message = message,
                TimeStamp = DateTime.Now,
                Log = log
            };
            this.provider.LogDetails.Add(newlogDetail);
            return newlogDetail;
        }

        public void Log(string message)
        {
            this.provider.Logs.Add(new Log { Message = message, TimeStamp = DateTime.Now });
            this.provider.UnitOfWork.Finished();
        }

        public void Log(Log log)
        {
            //imame log i zakacheni Details
            this.provider.UnitOfWork.Finished();
        }
    }
}