using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATPTennisStat.Common;
using ATPTennisStat.ConsoleClient.Core.Contracts;

namespace ATPTennisStat.ConsoleClient.Core.Commands.TicketCommands
{
    public class BuyTicketsCommand : ICommand
    {
        protected readonly PostgresDataProvider dp;

        public BuyTicketsCommand(PostgresDataProvider dp)
        {
            this.dp = dp;
        }

        public string Execute(IList<string> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
