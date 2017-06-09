using System.Collections.Generic;

namespace ATPTennisStat.ConsoleClient.Core.Contracts
{
    public interface IParser
    {
        string ParseCommand(string commandLine);
    }
}