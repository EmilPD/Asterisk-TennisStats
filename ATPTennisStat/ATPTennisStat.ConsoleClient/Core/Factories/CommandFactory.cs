using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATPTennisStat.Common;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.ConsoleClient.Core.Commands.TicketCommands;
using ATPTennisStat.SQLServerData;
using ATPTennisStat.Factories.Contracts;
using ATPTennisStat.Factories;

namespace ATPTennisStat.ConsoleClient.Core.Factories
{
    class CommandFactory : ICommandFactory
    {
        private readonly ITicketModelsFactory factory;
        private readonly PostgresDataProvider PgDp;
        private readonly SqlServerDataProvider SqlDp;

        public CommandFactory(ITicketModelsFactory factory, PostgresDataProvider PgDp, SqlServerDataProvider SqlDp)
        {
            this.factory = factory ?? new TicketModelsFactory(PgDp);
            this.PgDp = PgDp;
            this.SqlDp = SqlDp;
        }

        public ICommand CreateCommandFromString(string commandName)
        {
            switch (commandName.ToLower())
            {
                case "tickets":
                    return this.ShowTicketsCommand(PgDp);
                case "buy":
                    return this.BuyTicketsCommand(PgDp);
                default:
                    throw new ArgumentException(nameof(ICommand));
            }
        }

        public ICommand ShowTicketsCommand(PostgresDataProvider PgDp)
        {
            return new ShowTicketsCommand(PgDp);
        }

        public ICommand BuyTicketsCommand(PostgresDataProvider PgDp)
        {
            return new BuyTicketsCommand(PgDp);
        }
    }
}
