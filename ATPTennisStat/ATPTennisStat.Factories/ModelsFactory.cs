using ATPTennisStat.Common;
using ATPTennisStat.Factories.Contracts;
using ATPTennisStat.Models;
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
        private SqlServerDataProvider dataProvider;

        public ModelsFactory(SqlServerDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }


        public Match CreateMatch(string datePlayed,
                         string winner,
                         string loser,
                         string result,
                         string tournamentName,
                         string round)
        {

            //TODO: MATCH EXISTS CONDITION 

            if (String.IsNullOrEmpty(winner))
            {
                throw new ArgumentException("Winner - null or empty");
            }

            if (String.IsNullOrEmpty(loser))
            {
                throw new ArgumentException("Loser - null or empty");
            }

            var winnerToLower = winner.ToLower();

            var loserToLower = loser.ToLower();

            if (datePlayed == null)
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

            //bool playerExists = this.dataProvider.Players.GetAll()
            //                    .Any(p => p.FirstName.ToLower() == firstNameToLower &&
            //                                p.LastName.ToLower() == lastNameToLower);
            //if (playerExists)
            //{
            //    throw new ArgumentException("Player already in the database");


            //RESULT NULLABLE

            //RoundID NULLABLE
            //TournamentID NULLABLE



            return new Match
            {
                DatePlayed = datePlayedParsed
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


            bool pointDistributionExists = this.dataProvider.PointDistributions.GetAll()
                    .Any(pd => pd.TournamentCategory.Category.ToLower() == categoryNameToLower &&
                                pd.TournamentCategory.PlayersCount == playersNumberParsed &&
                                pd.Round.Stage == roundNameParsed);
            if (pointDistributionExists)
            {
                throw new ArgumentException("Point distribution already exists in the database");
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

            bool tournamentExists = this.dataProvider.Tournaments.GetAll()
                                .Any(t => t.Name.ToLower() == nameToLower);
            if (tournamentExists)
            {
                throw new ArgumentException("Tournament with the same name already in the database");
            }

            //START DATE
            if (startDate == null)
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
            if (endDate == null)
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

            if (prizeMoney == null)
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

            bool tournamentCategoryExists = this.dataProvider.TournamentCategories.GetAll()
                                .Any(tc => tc.Category.ToLower() == nameToLower &&
                                        tc.PlayersCount == playersCount);

            if (tournamentCategoryExists)
            {
                throw new ArgumentException("Category already in the database");
            }

            return new TournamentCategory
            {
                Category = categoryName,
                PlayersCount = playersCount
            };
        }


        public Player CreatePlayer(string firstName,
                                   string lastName,
                                   string ranking,
                                   string birthDate,
                                   string height,
                                   string weight,
                                   string cityName,
                                   string countryName)
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

            bool playerExists = this.dataProvider.Players.GetAll()
                                .Any(p => p.FirstName.ToLower() == firstNameToLower &&
                                            p.LastName.ToLower() == lastNameToLower);
            if (playerExists)
            {
                throw new ArgumentException("Player already in the database");
            }



            int rankingParsed;
            try
            {
                rankingParsed = int.Parse(ranking);
            }
            catch (Exception)
            {

                throw new ArgumentException("Ranking cannot be parsed");
            }


            DateTime? birthDateParsed = null;
            if (birthDate != null)
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



            float heightParsed;
            try
            {
                heightParsed = float.Parse(height);
            }
            catch (Exception)
            {

                throw new ArgumentException("Height cannot be parsed");
            }

            float weightParsed;
            try
            {
                weightParsed = float.Parse(weight);
            }
            catch (Exception)
            {

                throw new ArgumentException("Weight cannot be parsed");
            }

            if (String.IsNullOrEmpty(cityName))
            {
                throw new ArgumentException("City name - null or empty");
            }

            if (String.IsNullOrEmpty(countryName))
            {
                throw new ArgumentException("Country name - null or empty");
            }

            var cityNameToLowerCase = cityName.ToLower();

            var countryNameLowerCase = countryName.ToLower();


            var list = this.dataProvider.Cities.GetAll();

            var city = list.FirstOrDefault(c => c.Name.ToLower() == cityNameToLowerCase);

            if (city == null)
            {
                city = CreateCity(cityName, countryName);

                this.dataProvider.Cities.Add(city);
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
            bool countryExists = this.dataProvider.Countries.GetAll()
                                .Any(c => c.Name.ToLower() == nameToLower);

            if (countryExists)
            {
                throw new ArgumentException("Country already in the database");
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

            bool cityExists = this.dataProvider.Cities.GetAll()
                                .Any(c => c.Name.ToLower() == cityNameLowerCase);

            if (cityExists)
            {
                throw new ArgumentException("City already in the database");
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
