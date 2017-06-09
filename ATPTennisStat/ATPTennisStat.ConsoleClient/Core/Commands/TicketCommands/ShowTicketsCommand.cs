using System;
using System.Collections.Generic;
using System.Linq;
using ATPTennisStat.Common;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Models.Enums;

namespace ATPTennisStat.ConsoleClient.Core.Commands.TicketCommands
{
    public class ShowTicketsCommand : ICommand
    {
        protected readonly PostgresDataProvider dp;

        public ShowTicketsCommand(PostgresDataProvider dp)
        {
            this.dp = dp;
        }

        public string Execute(IList<string> parameters)
        {
            var result = "";
                var tevents = dp.TennisEvents.GetAll();
               
                foreach (var evt in tevents)
                {
                    result += $"*** {evt.Name} ***\n";
                    result += new String('-', evt.Name.Length + 8) + "\n";

                    var tList = dp
                        .Tickets
                        .Find(t => t.TennisEvent.Id == evt.Id)
                        .Select(t =>$"Id: {t.Id} | Price: {t.Price} | Sector(Sector): {t.Sector} | Remaining: {t.Number}")
                        .ToList();

                    var tickets = String.Join("\n    ", tList);
                    if (tickets.Length > 0)
                    {
                        result += "    " + tickets;
                    }
                    result += "\n\n";
                }
            result += "\n";
            return result;
        }
    }
}
