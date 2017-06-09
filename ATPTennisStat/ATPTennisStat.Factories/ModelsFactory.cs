using ATPTennisStat.Common;
using ATPTennisStat.Factories.Contracts;
using ATPTennisStat.Models;
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
            if (playerExists) {
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
            if(birthDate != null)
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
                city = CreateCity(cityName,countryName);

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


            //dbCtx.Entry(stud).State = System.Data.Entity.EntityState.Modified;

            if (country == null)
            {
                country = CreateCountry(countryName);
                    
                //    new Country
                //{
                //    Name = countryName
                //};
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
