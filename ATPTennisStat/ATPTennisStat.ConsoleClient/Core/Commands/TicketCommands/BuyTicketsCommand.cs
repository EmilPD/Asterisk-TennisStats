using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATPTennisStat.Common;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using Bytes2you.Validation;

namespace ATPTennisStat.ConsoleClient.Core.Commands.TicketCommands
{
    public class BuyTicketsCommand : ICommand
    {
        protected readonly PostgresDataProvider dp;
        private IWriter writer;

        public BuyTicketsCommand(PostgresDataProvider dp, IWriter writer)
        {
            this.dp = dp;
            this.writer = writer;
        }

        public string Execute(IList<string> parameters)
        {
            this.writer.Clear();
            var result = new StringBuilder();
            int ticketId = -1;

            int.TryParse(parameters[0], out ticketId);
            Guard.WhenArgument(ticketId, "Incorrect event Id!").IsLessThan(0).Throw();

            var currentTickets = dp.Tickets.Find(t => t.Id == ticketId).FirstOrDefault();
            if (currentTickets != null)
            {
                currentTickets.Number--;
                dp.UnitOfWork.Finished();
                result.AppendLine($"Successfully bought ticket for {currentTickets.TennisEvent.Name}!");
            }
            else
            {
                result.AppendLine("Sorry no tickets with this Id were found!");
            }
            
            result.AppendLine("[menu] [alle] [allt] [buyt (id)]");
            return result.ToString();
        }
    }
}
