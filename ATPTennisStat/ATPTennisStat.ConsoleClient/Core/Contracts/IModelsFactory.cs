using ATPTennisStat.Models.Enums;
using ATPTennisStat.Models.PostgreSqlModels;

namespace ATPTennisStat.ConsoleClient.Core.Contracts
{
    public  interface IModelsFactory
    {
        Ticket CreateTicket(int sector, double price, int number, int eventId);

        TennisEvent CreateTennisEvent(string name);

        // player

        // tournament

        // city

        // country
    }
}
