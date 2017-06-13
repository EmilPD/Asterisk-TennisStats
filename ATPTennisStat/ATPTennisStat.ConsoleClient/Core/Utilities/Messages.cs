using System;

namespace ATPTennisStat.ConsoleClient.Core.Utilities
{
    public static class Messages
    {
        internal const string ParametersWarning = "This command does not take any parameters";

        internal static string GenerateWelcomeMessage()
        {
            return @" < WELCOME TO ASTERISK - TENNIS STATS >
----------------------------------------
   Please input the command from the 
   options below and press <enter>
----------------------------------------";
        }

        internal static string GenerateMainMenu()
        {
            return @"
 [r] Tennis reporters
 [i] Import data
 [s] Tennis statistics menu
 [t] Ticket store menu
 [l] Show all logs
 [a] Team Asterisk info

 [exit]";
        }

        internal static string GenerateImportersMenu()
        {
            return @"
 [importm] Import matches
 [importp] Import Players
 [importpd] Tennis point distribution
 [importt] Import tournaments
 [importsd] Import sample data

 [menu]";
        }

        internal static string GenerateReportersMenu()
        {
            return @"
 [pdfm] Create PDF report for matches
 [pdfr] Create PDF report for ranking

 [menu]";
        }

        internal static string GenerateTicketMenu()
        {
            return @"
 [allt] Show all tickets
 [alle] Show all events
 [buyt (id)] buy a ticket with (id)

 [menu]";
        }

        internal static string GenerateDataMenu()
        {
            return @"
 [show] Show tennis data menu
 [add] Add tennis data menu

 [menu]";
        }

        internal static string GenerateDataAddMenu()
        {
            return @"
 [addco (name)] Add new country
 [addct (name) (country)] Add new city
 [addp (2 - 7 arguments)] Add new player
 [updatep (id)] Update Player with id
 [addt (4 - 10 arguments)] Add new tournament
 [addm (6 arguments)] Add new match
 [delm (id)] Delete Match with id

 [menu] [show]";
        }

        internal static string GenerateDataShowMenu()
        {
            return @"
 [showp (id)] Show all players of filter by id
 [showt (id)] Show all tournaments of filter by id
 [showm (id)] Show all mathes of filter by id

 [menu] [add]";
        }
    }
}
