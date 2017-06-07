using System;
using System.IO;
using System.Linq;
using System.Data.Entity;
using ClosedXML.Excel;
using Ninject;
using ATPTennisStat.ReportGenerators;
using ATPTennisStat.SQLServerData;
using ATPTennisStat.Models;
using ATPTennisStat.Repositories;
using ATPTennisStat.SQLServerData.Migrations;

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
            //ExcelImport();
            //NinjectStart();
            GeneratePdfReport();

        }

        private static void GeneratePdfReport()
        {
            var kernel = new StandardKernel(new ATPTennisStatModules());

            var report = kernel.Get<PdfReportGenerator>();
            report.GenerateReport();
        }

        private static void NinjectStart()
        {
            var kernel = new StandardKernel(new ATPTennisStatModules());

            var dp = kernel.Get<SqlServerDataProvider>();
            //var cities = dp.Cities.Find(c => c.Country.Name == "Bulgaria");

            var players = dp.Players.GetAll();

            //dp.cities.Add(new City
            //{
            //    Name = "Burgas",
            //    Country = new Country { Name = "Bulgaria" }
            //});

            //dp.unitOfWork.Finished();

            //foreach (var city in cities)
            //{
            //    Console.WriteLine(city.Name);
            //}

            foreach (var p in players)
            {
                Console.WriteLine("First name: {0}", p.FirstName);
                Console.WriteLine("LastName: {0}", p.LastName);
                Console.WriteLine("Ranking: {0}", p.Ranking);
                Console.WriteLine("City of birth: {0}", p.City.Name);
                Console.WriteLine("Country of birth: {0}", p.City.Country.Name);
                Console.WriteLine("-------------");
                Console.WriteLine();
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
    }
}
