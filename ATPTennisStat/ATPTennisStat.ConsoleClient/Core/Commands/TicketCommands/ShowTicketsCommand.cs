using System;
using System.Collections.Generic;
using System.Linq;
using ATPTennisStat.Common;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Models.Enums;
using System.Text;

namespace ATPTennisStat.ConsoleClient.Core.Commands.TicketCommands
{
    public class ShowTicketsCommand : ICommand
    {
        private const string NoTicketsMessage = "Sorry, no tickets for this event!";
        protected readonly PostgresDataProvider dp;
        private IWriter writer;

        public ShowTicketsCommand(PostgresDataProvider dp, IWriter writer)
        {
            this.dp = dp;
            this.writer = writer;
        }

        public string Execute(IList<string> parameters)
        {
            this.writer.Clear();
            var result = new StringBuilder();
            var tevents = dp.TennisEvents.GetAll();

            foreach (var evt in tevents)
            {
                result.AppendLine($"* {evt.Name}");

                var tList = dp
                    .Tickets
                    .Find(t => t.TennisEvent.Id == evt.Id)
                    .Select(t => $"Id: {t.Id} | Price: {t.Price} | Sector(Sector): {t.Sector} | Remaining: {t.Number}")
                    .ToList();

                var tickets = String.Join("\n    ", tList);

                if (tickets.Length > 0)
                {
                    result.AppendLine("    " + tickets);
                }
                else
                {
                    result.AppendLine("    " + NoTicketsMessage);
                }
                result.AppendLine("");   
            }
            result.AppendLine("[menu] [alle] [allt] [buyt (id)]");
            return result.ToString();
        }
    }
}
