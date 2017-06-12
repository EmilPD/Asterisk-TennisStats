using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.PostgreSqlData;
using System;

namespace ATPTennisStat.ConsoleClient.Core.Commands.TicketCommands
{
    public class ShowEventsCommand : ICommand
    {
        protected readonly IPostgresDataProvider dp;
        private IWriter writer;

        public ShowEventsCommand(IPostgresDataProvider dp, IWriter writer)
        {
            if (dp == null)
            {
                throw new ArgumentNullException("Data provider cannot be null!");
            }

            if (writer == null)
            {
                throw new ArgumentNullException("Writer cannot be null!");
            }

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
                var tCount = dp
                    .Tickets
                    .Find(t => t.TennisEvent.Id == evt.Id)
                    .Select(t => t.Number)
                    .ToList()
                    .Sum();

                result.AppendLine($"Id: {evt.Id} | Name: {evt.Name} | Remaining Tickets: {tCount}");
            }
            result.AppendLine("[menu] [alle] [allt] [buyt (id)]");
            return result.ToString();
        }
    }
}