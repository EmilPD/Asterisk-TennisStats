using System;
using System.Linq;
using ATPTennisStat.SQLServerData;
using ATPTennisStat.Models;
using ATPTennisStat.Repositories;
using System.Data.Entity;
using ATPTennisStat.SQLServerData.Migrations;
using System.IO;
using ClosedXML.Excel;
using Ninject;

namespace ATPTennisStat.ConsoleClient
{
    class Startup
    {
        static void Main()
        {
            Database.SetInitializer(
            new MigrateDatabaseToLatestVersion<SqlServerDbContext, Configuration>());
            ///<summary>
            ///Control Flow -> choose either of the following methods
            ///</summary>
            //DbContextStart();

            //RepoStart();
            //ExcelImport();
            NinjectStart();

        }

        private static void NinjectStart()
        {
            var kernel = new StandardKernel(new ATPTennisStatModules());

            var dp = kernel.Get<SqlServerDataProvider>();
            var cities = dp.cities.GetAll();

            //dp.cities.Add(new City
            //{
            //    Name = "Burgas",
            //    Country = new Country { Name = "Bulgaria" }
            //});

            dp.unitOfWork.Finished();

            foreach (var city in cities)
            {
                Console.WriteLine(city.Name);
                Console.WriteLine(city.Country.Name);
            }
        }

        static void ExcelImport()
        {
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;

            string path = dir + "\\Data\\Excel\\TennisStatsDatabase.xlsx";
            Console.WriteLine(path);
            var workbook = new XLWorkbook(path);
            var ws = workbook.Worksheet(1);
            Console.WriteLine(ws.Name);
            var currentRegion = ws.RangeUsed().AsTable();
            var names = currentRegion.DataRange.Rows()
                .Select(nameRow => nameRow.Field("Name").GetString())
                .ToList();
            names.ForEach(Console.WriteLine);
        }

        static void DbContextStart()
        {
            var context = new SqlServerDbContext();
            //context.Cities.Add(new City
            //{
            //    Name = "Varna",
            //    Country = new Country { Name = "Bulgaria" }
            //    //Players = new List<Player> { new Player { FirstName = "Ivan" } },
            //    //Tournaments = new List<Tournament> { new Tournament
            //    //    { Name = "Paris", PrizeMoney = 10 } }
            //});



            context.SaveChanges();

            var selectedCity = context.Cities
                .Where(c => c.Name == "Varna")
                .Select(c => new
                {
                    CityName = c.Name
                })
                .FirstOrDefault();
            Console.WriteLine(selectedCity);

        }

        static void RepoStart()
        {
            var unitOfWork = new SqlServerUnitOfWork(new SqlServerDbContext());
            var citiesCollection = unitOfWork.Cities.GetAll();
            foreach (var city in citiesCollection)
            {
                Console.WriteLine(city.Name);
            }

        }
    }
}
