using ATPTennisStat.Models.SqliteModels;
using System.Collections.Generic;

namespace ATPTennisStat.ConsoleClient.Core.Contracts
{
    public interface ILogger
    {
        void Log(string message);

        Log CreateNewLog(string command);

        LogDetail CreateNewLogDetail(string message, Log log);

        void Log(Log log);
    }
}
