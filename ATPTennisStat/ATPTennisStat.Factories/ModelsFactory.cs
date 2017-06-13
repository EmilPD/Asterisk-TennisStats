using ATPTennisStat.Common;
using ATPTennisStat.Factories.Contracts;
using ATPTennisStat.Models.SqlServerModels;
using ATPTennisStat.Models.Enums;
using ATPTennisStat.SQLServerData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.Factories
{
    public class ModelsFactory : IModelsFactory
    {
        private ISqlServerDataProvider dataProvider;

        public ModelsFactory(ISqlServerDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public Match CreateMatch(string datePlayed,
                         string winnerName,
                         string loserName,
                         string result,
                         string tournamentName,
                         string roundName)
        {


            if (String.IsNullOrEmpty(winnerName))
            {
                throw new ArgumentException("Winner - null or empty");
            }

            if (String.IsNullOrEmpty(loserName))
            {
                throw new ArgumentException("Loser - null or empty");
            }
            string[] winnerNames = winnerName.Split(' ');

            if (winnerNames.Length < 2)
            {
                throw new ArgumentException("Winner name not formatted correctly - Firstname Lastname");
            }
            string[] loserNames = loserName.Split(' ');

            if (loserNames.Length < 2)
            {
                throw new ArgumentException("Loser name not formatted correctly - Firstname Lastname");
            }

            var winnerFirstName = winnerNames[0];
            var winnerFirstNameToLower = winnerFirstName.ToLower();
            var winnerLastName = string.Join(" ", winnerNames.Skip(1));
            var winnerLastNameToLower = winnerLastName.ToLower();

            var loserFirstName = loserNames[0];
            var loserFirstNameToLower = loserFirstName.ToLower();
            var loserLastName = string.Join(" ", loserNames.Skip(1));
            var loserLastNameToLower = loserLastName.ToLower();


            if (String.IsNullOrEmpty(datePlayed))
            {
                throw new ArgumentException("Match date is required");
            }

            DateTime datePlayedParsed;

            try
            {
                datePlayedParsed = DateTime.Parse(datePlayed);
            }
            catch (Exception)
            {

                throw new ArgumentException("Startdate cannot be parsed");
            }

            var matchFound = this.dataProvider.Matches.GetAll()
                                .FirstOrDefault(m => m.Winner.FirstName.ToLower() == winnerFirstNameToLower &&
                                          m.Winner.LastName.ToLower() == winnerLastNameToLower &&
                                          m.Loser.FirstName.ToLower() == loserFirstNameToLower &&
                                          m.Loser.LastName.ToLower() == loserLastNameToLower &&
                                          m.DatePlayed == datePlayedParsed); // working?
            if (matchFound != null)
            {
                throw new ArgumentException($"Match between {matchFound.Winner.LastName} and {matchFound.Loser.LastName} played on {matchFound.DatePlayed} already exists in the database under id: {matchFound.Id}");
            }


            if (String.IsNullOrEmpty(result))
            {
                throw new ArgumentException("Result - null or empty");
            }

            var winner = this.dataProvider.Players.GetAll()
                            .FirstOrDefault(p => p.FirstName.ToLower() == winnerFirstNameToLower &&
                                                 p.LastName.ToLower() == winnerLastNameToLower);

            if (String.IsNullOrEmpty(roundName))
            {
                throw new ArgumentException("Round - null or empty");
            }

            if (String.IsNullOrEmpty(tournamentName))
            {
                throw new ArgumentException("Tournament - null or empty");
            }


            if (winner == null)
            {
                winner = CreatePlayer(winnerFirstName, winnerLastName);

                this.dataProvider.Players.Add(winner);
            }

            var loser = this.dataProvider.Players.GetAll()
                .FirstOrDefault(p => p.FirstName.ToLower() == loserFirstNameToLower &&
                                     p.LastName.ToLower() == loserLastNameToLower);



            if (loser == null)
            {
                loser = CreatePlayer(loserFirstName, loserLastName);

                this.dataProvider.Players.Add(loser);
            }

            //RoundID
            RoundStage roundParsed;

            try
            {
                roundParsed = (RoundStage)Enum.Parse(typeof(RoundStage), roundName, true);
            }
            catch (Exception)
            {

                throw new ArgumentException("Round stage cannot be parsed - Available ones are Q1, Q2...");
            }

            var round = dataProvider.Rounds.GetAll()
                            .FirstOrDefault(r => r.Stage == roundParsed);

            if (round == null)
            {
                round = new Round
                {
                    Stage = roundParsed
                };

                this.dataProvider.Rounds.Add(round);
            }
            //TournamentID
            var tournamentNameToLower = tournamentName.ToLower();

            var tournament = dataProvider.Tournaments.GetAll()
                             .FirstOrDefault(t => t.Name.ToLower() == tournamentNameToLower);

            if (tournament == null)
            {
                throw new ArgumentException("Tournament cannot be found - please import tournaments" + tournamentName);
            }

            //Console.WriteLine("MatchImported " + tournamentName);

            return new Match
            {
                DatePlayed = datePlayedParsed,
                Winner = winner,
                Loser = loser,
                Result = result,
                Round = round,
                Tournament = tournament
            };
        }


        public PointDistribution CreatePointDistribution(string categoryName,
                                                         string playersNumber,
                                                         string roundName,
                                                         string points)
        {


            if (String.IsNullOrEmpty(categoryName))
            {
                throw new ArgumentException("Category name is empty");
            }

            var categoryNameToLower = categoryName.ToLower();

            if (String.IsNullOrEmpty(roundName))
            {
                throw new ArgumentException("Round name is empty");
            }

            RoundStage roundNameParsed;

            try
            {
                roundNameParsed = (RoundStage)Enum.Parse(typeof(RoundStage), roundName, true);
            }
            catch (Exception)
            {

                throw new ArgumentException("Round stage cannot be parsed - Available ones are Q1, Q2...");
            }

            if (String.IsNullOrEmpty(playersNumber))
            {
                throw new ArgumentException("Players number is empty");
            }
            byte playersNumberParsed;

            try
            {
                playersNumberParsed = byte.Parse(playersNumber);
            }
            catch (Exception)
            {

                throw new ArgumentException("Players number cannot be parsed");
            }


            var foundPointDistribution = this.dataProvider.PointDistributions.GetAll()
                    .FirstOrDefault(pd => pd.TournamentCategory.Category.ToLower() == categoryNameToLower &&
                                pd.TournamentCategory.PlayersCount == playersNumberParsed &&
                                pd.Round.Stage == roundNameParsed);
            if (foundPointDistribution != null)
            {
                throw new ArgumentException($"Point distribution for already exists in the database under round id: {foundPointDistribution.RoundId} and tournament category id: {foundPointDistribution.TournamentCategoryId} ");
            }

            int pointsParsed;

            try
            {
                pointsParsed = int.Parse(points);
            }
            catch (Exception)
            {

                throw new ArgumentException("Points cannot be parsed");
            }

            var tournamentCategory = dataProvider.TournamentCategories.GetAll()
                                        .FirstOrDefault(c => c.Category.ToLower() == categoryNameToLower &&
                                            c.PlayersCount == playersNumberParsed);



            if (tournamentCategory == null)
            {
                tournamentCategory = CreateTournamentCategory(categoryName, playersNumberParsed);

                this.dataProvider.TournamentCategories.Add(tournamentCategory);
            }

            var round = dataProvider.Rounds.GetAll()
                                        .FirstOrDefault(r => r.Stage == roundNameParsed);

            if (round == null)
            {
                round = new Round
                {
                    Stage = roundNameParsed
                };

                this.dataProvider.Rounds.Add(round);
            }

            return new PointDistribution
            {
                TournamentCategory = tournamentCategory,
                Round = round,
                Points = pointsParsed

            };
        }


        public Tournament CreateTournament(string name,
                                           string startDate,
                                           string endDate,
                                           string prizeMoney,
                                           string categoryName,
                                           string playersCount,
                                           string cityName,
                                           string countryName,
                                           string surfaceType,
                                           string surfaceSpeed)
        {

            //Name is Unique

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Tournament name is empty");
            }


            var nameToLower = name.ToLower();

            var tournamentFound = this.dataProvider.Tournaments.GetAll()
                                .FirstOrDefault(t => t.Name.ToLower() == nameToLower);
            if (tournamentFound != null)
            {
                throw new ArgumentException($"{tournamentFound.Name} already in the database under id {tournamentFound.Id}");
            }

            //START DATE
            if (String.IsNullOrEmpty(startDate))
            {
                throw new ArgumentException("Tournament startdate is required");
            }


            DateTime startDateParsed;

            try
            {
                startDateParsed = DateTime.Parse(startDate);
            }
            catch (Exception)
            {

                throw new ArgumentException("Startdate cannot be parsed");
            }

            //END DATE
            if (String.IsNullOrEmpty(endDate))
            {
                throw new ArgumentException("Tournament enddate is required");
            }

            DateTime endDateParsed;

            try
            {
                endDateParsed = DateTime.Parse(endDate);
            }
            catch (Exception)
            {

                throw new ArgumentException("Enddate cannot be parsed");
            }

            //prizeMoney

            if (String.IsNullOrEmpty(prizeMoney))
            {
                throw new ArgumentException("Prize money is required");
            }

            decimal prizeMoneyParsed;

            try
            {
                prizeMoneyParsed = decimal.Parse(prizeMoney);
            }
            catch (Exception)
            {

                throw new ArgumentException("Prize money cannot be parsed");
            }

            //look for existing category -> + PlayersCount
            //categoryName
            if (String.IsNullOrEmpty(categoryName))
            {
                throw new ArgumentException("Category name is required");
            }

            if (String.IsNullOrEmpty(playersCount))
            {
                throw new ArgumentException("Players number is required");
            }

            var categoryNameToLower = categoryName.ToLower();

            byte playersCountParsed;

            try
            {
                playersCountParsed = byte.Parse(playersCount);
            }
            catch (Exception)
            {
                throw new ArgumentException("Prize money cannot be parsed");
            }


            var list = this.dataProvider.TournamentCategories.GetAll();

            var tournamentCategory = list.FirstOrDefault(c => c.Category.ToLower() == categoryNameToLower &&
                                            c.PlayersCount == playersCountParsed);



            if (tournamentCategory == null)
            {
                tournamentCategory = CreateTournamentCategory(categoryName, playersCountParsed);

                this.dataProvider.TournamentCategories.Add(tournamentCategory);
            }

            //create new city
            if (String.IsNullOrEmpty(cityName))
            {
                throw new ArgumentException("City name is required");
            }

            if (String.IsNullOrEmpty(countryName))
            {
                throw new ArgumentException("Country name is required");
            }

            var cityNameToLower = cityName.ToLower();

            var countryNameToLower = countryName.ToLower();

            var city = this.dataProvider.Cities.GetAll()
                        .FirstOrDefault(c => c.Name.ToLower() == cityNameToLower);

            if (city == null)
            {
                city = CreateCity(cityName, countryName);

                this.dataProvider.Cities.Add(city);
            }

            //surface type + surface speed

            if (String.IsNullOrEmpty(surfaceType))
            {
                throw new ArgumentException("Surface type is required");
            }

            if (String.IsNullOrEmpty(surfaceSpeed))
            {
                throw new ArgumentException("Surface speed is required");
            }

            var surfaceTypeToLower = surfaceType.ToLower();

            var surfaceSpeedToLower = surfaceSpeed.ToLower();

            var surfacesList = this.dataProvider.Surfaces.GetAll();

            var surface = surfacesList
                                .FirstOrDefault(s => s.Type.ToLower() == surfaceTypeToLower &&
                                            s.Speed.ToLower() == surfaceSpeedToLower);

            if (surface == null)
            {
                surface = CreateSurface(surfaceType, surfaceSpeed);

                this.dataProvider.Surfaces.Add(surface);
            }


            return new Tournament
            {
                Name = name,
                StartDate = startDateParsed,
                EndDate = endDateParsed,
                PrizeMoney = prizeMoneyParsed,
                Category = tournamentCategory,
                City = city,
                Type = surface

            };
        }

        public Surface CreateSurface(string surfaceType, string surfaceSpeed)
        {

            if (String.IsNullOrEmpty(surfaceType))
            {
                throw new ArgumentException("Surface type - null or empty");
            }

            if (String.IsNullOrEmpty(surfaceType))
            {
                throw new ArgumentException("Surface speed - null or empty");
            }

            var surfaceTypeToLower = surfaceType.ToLower();
            var surfaceSpeedToLower = surfaceSpeed.ToLower();

            bool surfaceExists = this.dataProvider.Surfaces.GetAll()
                                .Any(s => s.Type.ToLower() == surfaceTypeToLower &&
                                            s.Speed.ToLower() == surfaceSpeedToLower);

            if (surfaceExists)
            {
                throw new ArgumentException("Surface already in the database");
            }


            return new Surface
            {
                Type = surfaceType,
                Speed = surfaceSpeed
            };
        }

        public TournamentCategory CreateTournamentCategory(string categoryName, byte playersCount)
        {
            if (String.IsNullOrEmpty(categoryName))
            {
                throw new ArgumentException("Category name - null or empty");
            }

            var nameToLower = categoryName.ToLower();

            var tournamentCategoryFound = this.dataProvider.TournamentCategories.GetAll()
                                .FirstOrDefault(tc => tc.Category.ToLower() == nameToLower &&
                                        tc.PlayersCount == playersCount);

            if (tournamentCategoryFound != null)
            {
                throw new ArgumentException($"{tournamentCategoryFound.Category} already exists in the database under id: {tournamentCategoryFound.Id}");
            }

            return new TournamentCategory
            {
                Category = categoryName,
                PlayersCount = playersCount
            };
        }


        public Player CreatePlayer(string firstName,
                                   string lastName,
                                   string ranking = null,
                                   string birthDate = null,
                                   string height = null,
                                   string weight = null,
                                   string cityName = null,
                                   string countryName = null)
        {


            if (String.IsNullOrEmpty(firstName))
            {
                throw new ArgumentException("First name - null or empty");
            }

            if (String.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("Last name - null or empty");
            }

            var firstNameToLower = firstName.ToLower();
            var lastNameToLower = lastName.ToLower();

            var playerFound = this.dataProvider.Players.GetAll()
                                .FirstOrDefault(p => p.FirstName.ToLower() == firstNameToLower &&
                                            p.LastName.ToLower() == lastNameToLower);
            if (playerFound != null)
            {
                throw new ArgumentException($"{playerFound.FirstName} {playerFound.LastName} already in the database under id {playerFound.Id}");
            }


            int? rankingParsed = null;

            if (String.IsNullOrEmpty(ranking) == false)
            {
                try
                {
                    rankingParsed = int.Parse(ranking);
                }
                catch (Exception)
                {

                    throw new ArgumentException("Ranking cannot be parsed");
                }
            }

            DateTime? birthDateParsed = null;
            if (String.IsNullOrEmpty(birthDate) == false)
            {
                try
                {
                    birthDateParsed = DateTime.Parse(birthDate);
                }
                catch (Exception)
                {

                    throw new ArgumentException("Birthdate cannot be parsed");
                }
            }



            float? heightParsed = null;
            if (String.IsNullOrEmpty(height) == false)
            {
                try
                {
                    heightParsed = float.Parse(height);
                }
                catch (Exception)
                {

                    throw new ArgumentException("Height cannot be parsed");
                }
            }


            float? weightParsed = null;
            if (String.IsNullOrEmpty(weight) == false)
            {
                try
                {
                    weightParsed = float.Parse(weight);
                }
                catch (Exception)
                {

                    throw new ArgumentException("Weight cannot be parsed");
                }
            }

            City city = null;

            if (String.IsNullOrEmpty(cityName) == false)
            {
                if (String.IsNullOrEmpty(countryName))
                {
                    throw new ArgumentException("Country name - null or empty");
                }

                var cityNameToLowerCase = cityName.ToLower();

                var countryNameLowerCase = countryName.ToLower();

                var list = this.dataProvider.Cities.GetAll();

                city = list.FirstOrDefault(c => c.Name.ToLower() == cityNameToLowerCase);

                if (city == null)
                {
                    city = CreateCity(cityName, countryName);

                    this.dataProvider.Cities.Add(city);
                }
            }

            return new Player
            {
                FirstName = firstName,
                LastName = lastName,
                Ranking = rankingParsed,
                BirthDate = birthDateParsed,
                Height = heightParsed,
                Weight = weightParsed,
                City = city

            };
        }

        public Country CreateCountry(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Country name - null or empty");
            }

            var nameToLower = name.ToLower();
            var countryFound = this.dataProvider.Countries.GetAll()
                                .FirstOrDefault(c => c.Name.ToLower() == nameToLower);

            if (countryFound != null)
            {
                throw new ArgumentException($"{countryFound.Name} already exists in the database under {countryFound.Id}");
            }


            return new Country
            {
                Name = name
            };
        }

        public City CreateCity(string name, string countryName)
        {

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("City name - null or empty");
            }

            var cityNameLowerCase = name.ToLower();

            var cityFound = this.dataProvider.Cities.GetAll()
                                .FirstOrDefault(c => c.Name.ToLower() == cityNameLowerCase);

            if (cityFound != null)
            {
                throw new ArgumentException($"{cityFound.Name} already in the database under id: {cityFound.Id}");
            }

            if (String.IsNullOrEmpty(countryName))
            {
                throw new ArgumentException("Country name - null or empty");
            }

            var countryNameLowerCase = countryName.ToLower();


            var list = this.dataProvider.Countries.GetAll();

            var country = list.FirstOrDefault(c => c.Name.ToLower() == countryNameLowerCase);

            if (country == null)
            {
                country = CreateCountry(countryName);

                this.dataProvider.Countries.Add(country);
            }

            return new City
            {
                Name = name,
                Country = country
            };

        }
    }
}
