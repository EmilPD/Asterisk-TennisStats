using System;
using ATPTennisStat.Models.Enums;
using ATPTennisStat.Models.PostgreSqlModels;
using Bytes2you.Validation;
using ATPTennisStat.Factories.Contracts;

namespace ATPTennisStat.Factories
{
    public partial class ModelsFactory : IModelsFactory
    {
        public Ticket CreateTicket(string sectorStr, string priceStr, string numberStr, string eventIdStr)
        {
            Sector sector;
            Decimal price = -1m;
            int number = -1;
            int eventId = -1;

            sector = (Sector) Enum.Parse(typeof(Sector), eventIdStr);
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
                Sector = (Sector) sector,
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
