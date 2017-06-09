using System;
using ATPTennisStat.ConsoleClient.Core.Contracts;

namespace ATPTennisStat.ConsoleClient.Core.Providers
{
    public class SqLiteLogger : ILogger
    {
        public void Log(object msg)
        {
            // TODO: implement to sqLite
            Console.WriteLine($"Logging {msg}!");
        }
    }
}
