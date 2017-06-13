using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using Bytes2you.Validation;
using ATPTennisStat.PostgreSqlData;
using System;

namespace ATPTennisStat.ConsoleClient.Core.Commands.TicketCommands
{
    public class BuyTicketsCommand : ICommand
    {
        protected readonly IPostgresDataProvider dp;
        private IWriter writer;

        public BuyTicketsCommand(IPostgresDataProvider dp, IWriter writer)
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

        public string Execute()
        {
            throw new ArgumentException("You need to pass a parameter to this command");
        }

        public string Execute(IList<string> parameters)
        {

            if (parameters.Count == 0)
            {
                return this.Execute();
            }

            else
            {
                this.writer.Clear();

                var result = new StringBuilder();
                int ticketId = -1;

                int.TryParse(parameters[0], out ticketId);
                Guard.WhenArgument(ticketId, "Incorrect event Id!").IsLessThan(0).Throw();

                var currentTickets = dp.Tickets.GetAllQuerable()
                                    .Where(t => t.Id == ticketId)
                                    .FirstOrDefault();

                
                if (currentTickets != null)
                {
                    var tennisEventName = dp.TennisEvents.GetAllQuerable()
                                            .Where(te => te.Id == currentTickets.TennisEventId)
                                            .Select(te => te.Name)
                                            .FirstOrDefault();

                    currentTickets.Number--;
                    dp.UnitOfWork.Finished();
                    result.AppendLine($"Successfully bought ticket for {tennisEventName}!");
                }
                else
                {
                    result.AppendLine($"Sorry no tickets with this Id: {ticketId} were found!");
                }

                result.AppendLine("[menu] [alle] [allt] [buyt (id)]");
                return result.ToString();
            }

        }
    }
}