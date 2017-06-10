using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.ConsoleClient.Core.Utilities
{
    public static class Messages
    {
        public static string GenerateWelcomeMessage()
        {
            return @" < WELCOME TO ASTERISK - TENNIS STATS >
----------------------------------------
   Please input the command from the 
   options below and press <enter>
----------------------------------------";
        }

        public static string GenerateMainMenu()
        {
            return @"
 [p] Tennis Players Menu
 [s] Tennis Statistics Menu
 [t] Ticket Store Menu
 [i] Team Asterisk Info";
        }

        public static string GenerateTicketMenu()
        {
            return @"
 [allt] Show All Tickets
 [alle] Show All Events
 [buyt (id)] Buy a ticket with (id)
 [menu] Back to main menu";
        }

        public static string GenerateDataAddMenu()
        {
            return @"
 [addp] Add New Player
 [addt] Add New Tournament
 [addm] Add New Match
 [menu] Back to main menu";
        }
    }
}
