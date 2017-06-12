using ATPTennisStat.ConsoleClient.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.ConsoleClient.Core.Commands.Contracts
{
    public interface ICommandNoParameters : ICommand
    {
        string Execute();
    }
}
