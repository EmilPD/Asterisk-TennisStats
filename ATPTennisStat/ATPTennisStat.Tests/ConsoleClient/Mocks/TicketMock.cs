using ATPTennisStat.Models.PostgreSqlModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.Tests.ConsoleClient.Mocks
{
    class TicketMock : Ticket
    {
        public TicketMock() : base()
        {

        }

        public int Number { get; set; }
    }
}
