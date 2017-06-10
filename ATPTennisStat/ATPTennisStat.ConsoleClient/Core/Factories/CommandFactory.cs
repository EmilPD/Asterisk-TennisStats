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
using ATPTennisStat.ConsoleClient.Core.Commands.ReporterCommands;
using ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataShowCommands;
using ATPTennisStat.PostgreSqlData;

namespace ATPTennisStat.ConsoleClient.Core.Factories
{
    class CommandFactory : ICommandFactory
    {
        private readonly PostgresDataProvider pgDp;
        private readonly SqlServerDataProvider sqlDp;
        private IWriter writer;
        private IModelsFactory modelsFactory;

        public CommandFactory(IWriter writer, PostgresDataProvider pgDp, SqlServerDataProvider sqlDp, IModelsFactory modelsFactory)
        {
            this.pgDp = pgDp;
            this.sqlDp = sqlDp;
            this.writer = writer;
            this.modelsFactory = modelsFactory;
        }

        public ICommand CreateCommandFromString(string commandName)
        {
            switch (commandName.ToLower())
            {
                // main menu
                case "menu":
                    return this.MainMenuCommand();
                case "r":
                    return this.ReportersMenuCommand();
                case "s":
                    return this.TennisDataMenuCommand();
                case "t":
                    return this.TicketMenuCommand();
                case "i":
                    return this.TeamInfoCommand();
                // tickets commands
                case "alle":
                    return this.ShowEventsCommand();
                case "allt":
                    return this.ShowTicketsCommand();
                case "buyt":
                    return this.BuyTicketsCommand();
                // reporter commands
                case "pdfm":
                    return this.CreateMatchesPdf();
                case "pdfr":
                    return this.CreateRankingPdf();
                // data menu
                case "show":
                    return this.ShowTennisDataMenuCommand();
                case "add":
                    return this.AddTennisDataMenuCommand();
                // data show
                case "showp":
                    return this.ShowPlayers();
                case "showt":
                    return this.ShowTournaments();
                case "showm":
                    return this.ShowMatches();
                // data add
                case "addco":
                    return this.AddCountry();
                case "addct":
                    return this.AddCity();
                case "addp":
                    return this.AddPlayer();
                case "addt":
                    return this.AddTournament();
                case "addm":
                    return this.AddMatch();
                default:
                    throw new ArgumentException(nameof(ICommand));
            }
        }

        // Menu commands
        public ICommand MainMenuCommand()
        {
            return new MainMenuCommand(writer);
        }

        public ICommand ReportersMenuCommand()
        {
            return new ReportersMenuCommand(writer);
        }

        public ICommand TicketMenuCommand()
        {
            return new TicketMenuCommand(writer);
        }

        public ICommand TeamInfoCommand()
        {
            return new TeamInfoCommand(writer);
        }

        // Reporters commands
        public ICommand CreateMatchesPdf()
        {
            return new CreateMatchesPdf(sqlDp, writer);
        }

        public ICommand CreateRankingPdf()
        {
            return new CreateRankingPdf(sqlDp, writer);
        }

        // Ticket store commands
        public ICommand ShowTicketsCommand()
        {
            return new ShowTicketsCommand(pgDp, writer);
        }

        public ICommand ShowEventsCommand()
        {
            return new ShowEventsCommand(pgDp, writer);
        }

        public ICommand BuyTicketsCommand()
        {
            return new BuyTicketsCommand(pgDp, writer);
        }

        // Tennis Data menu commands
        public ICommand TennisDataMenuCommand()
        {
            return new TennisDataMenuCommand(writer);
        }

        public ICommand ShowTennisDataMenuCommand()
        {
            return new ShowTennisDataMenuCommand(writer);
        }

        public ICommand AddTennisDataMenuCommand()
        {
            return new AddTennisDataMenuCommand(writer);
        }

        // Data Add Commands
        public ICommand AddCountry()
        {
            return new AddCountryCommand(sqlDp, writer);
        }

        public ICommand AddCity()
        {
            return new AddCityCommand(sqlDp, writer);
        }

        public ICommand AddPlayer()
        {
            return new AddPlayerCommand(sqlDp, writer, modelsFactory);
        }

        public ICommand AddTournament()
        {
            return new AddTournamentCommand(sqlDp, writer, modelsFactory);
        }

        public ICommand AddMatch()
        {
            return new AddMatchCommand(sqlDp, writer, modelsFactory);
        }

        // Data Show Commands
        public ICommand ShowTournaments()
        {
            return new ShowTournamentsCommand(sqlDp, writer);
        }

        public ICommand ShowMatches()
        {
            return new ShowMatchesCommand(sqlDp, writer);
        }

        public ICommand ShowPlayers()
        {
            return new ShowPlayersCommand(sqlDp, writer);
        }
    }
}
