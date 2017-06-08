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
    public class ModelsFactory
    {
        private SqlServerDataProvider dataProvider;

        public ModelsFactory(SqlServerDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public City CreateCity(string name, string countryName)
        {
            var countryNameLowerCase = countryName.ToLower();
            var cityNameLowerCase = name.ToLower();

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("City name - null or empty");
            }

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

            var list = this.dataProvider.Countries.GetAll();

            var country = list.FirstOrDefault(c => c.Name.ToLower() == countryNameLowerCase);


            //dbCtx.Entry(stud).State = System.Data.Entity.EntityState.Modified;

            if (country == null)
            {
                country = new Country
                {
                    Name = countryName
                };
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
