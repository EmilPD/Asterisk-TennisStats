using System;
using ATPTennisStat.Models.Enums;
using ATPTennisStat.Models.PostgreSqlModels;
using ATPTennisStat.Factories.Contracts;
using ATPTennisStat.PostgreSqlData;
using Bytes2you.Validation;

namespace ATPTennisStat.Factories
{
    public class TicketModelsFactory : ITicketModelsFactory
    {
        private PostgresDataProvider postgreDataProvider;

        public TicketModelsFactory(PostgresDataProvider postgreDataProvider)
        {
            this.postgreDataProvider = postgreDataProvider;
        }

        public Ticket CreateTicket(string sectorStr, string priceStr, string numberStr, string eventIdStr)
        {
            Sector sector;
            Decimal price = -1m;
            int number = -1;
            int eventId = -1;

            sector = (Sector) Enum.Parse(typeof(Sector), sectorStr);
            Decimal.TryParse(priceStr, out price);
            int.TryParse(numberStr, out number);
            int.TryParse(eventIdStr, out eventId);

            if (!Enum.IsDefined(typeof(Sector), sector))
            {
                throw new ArgumentException("Sector is not valid!");    
            }

            Guard.WhenArgument(price, "Ticket price").IsLessThan(0).Throw();
            Guard.WhenArgument(number, "Number of available tickets").IsLessThan(0).Throw();
            Guard.WhenArgument(eventId, "Event Id").IsLessThan(0).Throw();

            var ticket = new Ticket()
            {
                Sector = sector,
                Price = price,
                Number = number,
                TennisEventId = eventId
            };

            return ticket;
        }

        public TennisEvent CreateTennisEvent(string name)
        {
            Guard.WhenArgument(name, "Event name").IsNullOrEmpty().Throw();
            var tennisEvent = new TennisEvent() {Name = name};
            return tennisEvent;
        }
    }
}
