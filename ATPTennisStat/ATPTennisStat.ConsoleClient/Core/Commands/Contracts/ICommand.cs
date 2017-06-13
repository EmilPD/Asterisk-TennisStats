using System.Collections.Generic;

namespace ATPTennisStat.ConsoleClient.Core.Contracts
{
    public interface ICommand
    {
        string Execute(IList<string> parameters);

        string Execute();
    }
}