using System;
using System.IO;
using System.Linq;
using System.Data.Entity;
using Ninject;
using ATPTennisStat.Importers;
using ATPTennisStat.ReportGenerators;
using ATPTennisStat.SQLServerData;
using ATPTennisStat.Models;
using ATPTennisStat.Repositories;
using ATPTennisStat.SQLServerData.Migrations;
using ATPTennisStat.Common;
using ATPTennisStat.Models.PostgreSqlModels;
using ATPTennisStat.Models.Enums;
using ATPTennisStat.PostgreSqlData;
using ATPTennisStat.Common.Enums;

namespace ATPTennisStat.ConsoleClient
{
    class Startup
    {
        static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SqlServerDbContext, SQLServerData.Migrations.Configuration>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PostgresDbContext, PostgreSqlData.Migrations.Configuration>());

            ///<summary>
            ///Control Flow -> choose either of the following methods
            ///</summary>
            //DbContextStart();
            ExcelImporter();
            //NinjectStart();
            //GeneratePdfReport();
            //PostgreDataStart();
        }

        private static void PostgreDataStart()
        {
            // TODO: Resolve duplicate DBContexts in Kernel???
            var kernel = new StandardKernel(new ATPTennisStatModules(DbContextType.Postgre));
            var dp = kernel.Get<PostgresDataProvider>();

            // test
            // Seed Data
            using (var uow = dp.UnitOfWork())
            {
                string[] names = new string[] { "Wimbledon - Final", "US Open - Q1", "Sofia Open - Semi-Final", "AO - R32", "Selski Turnir" };
                foreach (var name in names)
                {
                    var tevent = new TennisEvent()
                    {
                        Name = name
                    };

                    dp.TennisEvents.Add(tevent);
                }

                for (int i = 1; i <= 10; i++)
                {
                    var ticket = new Ticket()
                    {
                        Sector = Sector.Front,
                        Price = 5.40m + i/2,
                        Number = 200 - i,
                        TennisEventId = i % 5 + 1
                    };
                    dp.Tickets.Add(ticket);
                }

                uow.Finished();
            }

            // Read Data
            var tevents = dp.TennisEvents.GetAll().ToList();

            foreach (var evt in tevents)
            {
                Console.WriteLine($"*** {evt.Name} ***");

                var tList = dp
                    .Tickets
                    .Find(t => t.TennisEvent.Id == evt.Id)
                    .Select(t => "Id: " + t.Id + ", Price: " + t.Price + ", Sector: " + (Sector)t.Sector + " - Remaining: " + t.Number)
                    .ToList();

                var tickets = String.Join("\n    ", tList);
                if (tickets.Length > 0)
                {
                    Console.WriteLine("    " + tickets);
                }
            }
        }

        private static void ExcelImporter()
        {
            var kernel = new StandardKernel(new ATPTennisStatModules(DbContextType.SQLServer));

            var excelImporter = kernel.Get<ExcelImporter>();
            excelImporter.Read();
            //excelImporter.Write();

        }

        private static void GeneratePdfReport()
        {
            var kernel = new StandardKernel(new ATPTennisStatModules(DbContextType.SQLServer));
            var report = kernel.Get<PdfReportGenerator>();
            report.GenerateReport();
        }

        private static void NinjectStart()
        {
            var kernel = new StandardKernel(new ATPTennisStatModules(DbContextType.SQLServer));
            var dp = kernel.Get<SqlServerDataProvider>();
            //var cities = dp.Cities.Find(c => c.Country.Name == "Bulgaria");

            var players = dp.Players.GetAll();
            //dp.Players.Add()
            
            dp.Cities.Add(new City
            {
                Name = "Burgas",
                Country = new Country { Name = "Bulgaria" }
            });

            dp.UnitOfWork.Finished();

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
