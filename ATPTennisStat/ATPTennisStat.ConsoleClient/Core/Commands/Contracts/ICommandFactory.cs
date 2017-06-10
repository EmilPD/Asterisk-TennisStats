using System;
using ATPTennisStat.Common;
using ATPTennisStat.SQLServerData;

namespace ATPTennisStat.ConsoleClient.Core.Contracts
{
    public interface ICommandFactory
    {
        ICommand CreateCommandFromString(string commandName);

        ICommand MainMenuCommand();

        ICommand TicketMenuCommand();

        // Ticket data commands
        ICommand ShowEventsCommand();

        ICommand ShowTicketsCommand();

        ICommand BuyTicketsCommand();

        // Create data commands 
        ICommand AddCountry();

        ICommand AddCity();

        ICommand AddPlayer();

        ICommand AddTournament();

        ICommand AddMatch();

        // TODO: Update data commands

        // TODO: Delete data commands

        // Read data commands
        ICommand ShowTournaments();

        ICommand ShowMatches();

        ICommand ShowPlayers();
    }
}