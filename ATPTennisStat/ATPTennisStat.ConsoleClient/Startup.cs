using System;
using System.Linq;
using ATPTennisStat.SQLServerData;
using ATPTennisStat.Models;
using ATPTennisStat.Repositories;

namespace ATPTennisStat.ConsoleClient
{
    class Startup
    {
        static void Main()
        {
            //var unitOfWork = new SqlServerUnitOfWork(new SqlServerDbContext());

            var context = new SqlServerDbContext();
            //context.Cities.Add(new City
            //{
            //    Name = "Varna",
            //    Country = new Country { Name = "Bulgaria" }
            //    //Players = new List<Player> { new Player { FirstName = "Ivan" } },
            //    //Tournaments = new List<Tournament> { new Tournament
            //    //    { Name = "Paris", PrizeMoney = 10 } }
            //});

            //context.SaveChanges();

            var selectedCity = context.Cities
                .Where(c => c.Name == "Varna")
                .Select(c => new
                {
                    CityName = c.Name
                })
                .FirstOrDefault();
            Console.WriteLine(selectedCity);

            //var citiesCollection = unitOfWork.Cities.GetAll();
            //foreach (var city in citiesCollection)
            //{
            //    Console.WriteLine(city.Name);
            //}
        }
    }
}
