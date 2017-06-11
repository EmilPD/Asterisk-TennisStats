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

        Tournament CreateTournament(string name,
                                           string startDate,
                                           string endDate,
                                           string prizeMoney,
                                           string categoryName,
                                           string playersCount,
                                           string cityName,
                                           string countryName,
                                           string surfaceType,
                                           string surfaceSpeed);

        Surface CreateSurface(string surfaceType, string surfaceSpeed);

        TournamentCategory CreateTournamentCategory(string categoryName, byte playersCount);

        Match CreateMatch(string datePlayed,
                         string winnerName,
                         string loserName,
                         string result,
                         string tournamentName,
                         string roundName);

        PointDistribution CreatePointDistribution(string categoryName,
                                                  string playersNumber,
                                                  string roundName,
                                                  string points);
    }
}
