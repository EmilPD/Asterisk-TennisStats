using ATPTennisStat.ConsoleClient.Core.Contracts;
using System;

namespace ATPTennisStat.ConsoleClient.Core.Providers
{
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}