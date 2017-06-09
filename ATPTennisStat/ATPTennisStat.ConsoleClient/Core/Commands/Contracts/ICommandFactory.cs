using System;
using ATPTennisStat.Common;

namespace ATPTennisStat.ConsoleClient.Core.Contracts
{
    public interface ICommandFactory
    {
        ICommand CreateCommandFromString(string commandName);

        ICommand ShowTicketsCommand(PostgresDataProvider dp);

        ICommand BuyTicketsCommand(PostgresDataProvider dp);
    }
}