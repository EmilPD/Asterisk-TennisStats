using ATPTennisStat.Models.PostgreSqlModels;

namespace ATPTennisStat.Factories.Contracts
{
    public interface ITicketModelsFactory
    {
        Ticket CreateTicket(string sector, string price, string number, string eventId);

        TennisEvent CreateTennisEvent(string name);
    }
}
