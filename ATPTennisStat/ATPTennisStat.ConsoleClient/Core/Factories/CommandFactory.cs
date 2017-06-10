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
using ATPTennisStat.ConsoleClient.Core.Commands.MenuCommands;

namespace ATPTennisStat.ConsoleClient.Core.Factories
{
    class CommandFactory : ICommandFactory
    {
        private readonly PostgresDataProvider PgDp;
        private readonly SqlServerDataProvider SqlDp;
        private IWriter writer;

        public CommandFactory(IWriter writer, PostgresDataProvider PgDp, SqlServerDataProvider SqlDp)
        {
            this.PgDp = PgDp;
            this.SqlDp = SqlDp;
            this.writer = writer;
        }

        public ICommand CreateCommandFromString(string commandName)
        {
            switch (commandName.ToLower())
            {
                case "menu":
                    return this.MainMenuCommand();
                case "t":
                    return this.TicketMenuCommand();
                case "alle":
                    return this.ShowEventsCommand();
                case "allt":
                    return this.ShowTicketsCommand();
                case "buyt":
                    return this.BuyTicketsCommand();
                default:
                    throw new ArgumentException(nameof(ICommand));
            }
        }

        public ICommand ShowTicketsCommand()
        {
            return new ShowTicketsCommand(PgDp, writer);
        }

        public ICommand BuyTicketsCommand()
        {
            return new BuyTicketsCommand(PgDp, writer);
        }

        public ICommand AddCountry()
        {
            throw new NotImplementedException();
        }

        public ICommand AddCity()
        {
            throw new NotImplementedException();
        }

        public ICommand AddPlayer()
        {
            throw new NotImplementedException();
        }

        public ICommand AddTournament()
        {
            throw new NotImplementedException();
        }

        public ICommand AddMatch()
        {
            throw new NotImplementedException();
        }

        public ICommand ShowTournaments()
        {
            throw new NotImplementedException();
        }

        public ICommand ShowMatches()
        {
            throw new NotImplementedException();
        }

        public ICommand ShowPlayers()
        {
            throw new NotImplementedException();
        }

        public ICommand MainMenuCommand()
        {
            return new MainMenuCommand(writer);
        }

        public ICommand TicketMenuCommand()
        {
            return new TicketMenuCommand(writer);
        }

        public ICommand ShowEventsCommand()
        {
            return new ShowEventsCommand(PgDp, writer);
        }
    }
}
