using System;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Models.Enums;
using ATPTennisStat.Models.PostgreSqlModels;
using Bytes2you.Validation;

namespace ATPTennisStat.ConsoleClient.Core.Factories
{
    public class ModelsFactory : IModelsFactory
    {
        public Ticket CreateTicket(int sector, double price, int number, int eventId)
        {
            if (!Enum.IsDefined(typeof(Sector), sector))
            {
                throw new ArgumentException("Sector is not valid!");    
            }

            Guard.WhenArgument(price, "Ticket price").IsNaN().Throw();
            Guard.WhenArgument(number, "Number of avaulable tickets").IsLessThan(0).Throw();
            Guard.WhenArgument(eventId, "Event Id").IsLessThan(0).Throw();

            var ticket = new Ticket()
            {
                Sector = (Sector) sector,
                Price = (decimal)price,
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
