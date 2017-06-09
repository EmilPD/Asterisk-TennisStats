using ATPTennisStat.Models;
using ATPTennisStat.Models.PostgreSqlModels;

namespace ATPTennisStat.Factories.Contracts
{
    public interface IModelsFactory
    {
        Player CreatePlayer(string firstName,
                            string lastName,
                            string ranking,
                            string birthDate,
                            string height,
                            string weight,
                            string cityName,
                            string countryName);

        Country CreateCountry(string name);

        City CreateCity(string name, string CountryName);

        // tournament

        // match
    }
}
