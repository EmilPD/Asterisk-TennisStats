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
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.ReportGenerators.Enums;
using ATPTennisStat.SQLiteData;
using ATPTennisStat.Models.SqliteModels;

namespace ATPTennisStat.ConsoleClient
{
    class Startup
    {
        static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SqlServerDbContext, SQLServerData.Migrations.Configuration>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PostgresDbContext, PostgreSqlData.Migrations.Configuration>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SqliteDbContext, SQLiteData.Migrations.Configuration>(true));

            ///<summary>
            ///Control Flow -> choose either of the following methods
            ///</summary>
            //DbContextStart();
            //ExcelImporter();
            //NinjectStart();
            //GeneratePdfReport();
            ConsoleEngineStart();
            //SqliteStart();
            //JsonImportStart();
        }

        private static void JsonImportStart()
        {
            var kernel = new StandardKernel(new ATPTennisStatModules());
            var dp = kernel.Get<SqlServerDataProvider>();
            var countriesInDb = dp.Countries.GetAll();

            var baseDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            var jsonPath = "\\Data\\Json\\";
            var jsonFileName = "countries.json";
            var fullPath = baseDir + jsonPath + jsonFileName;

            var jsonImporter = new JSONImporter(fullPath);
            var listOfCountries = jsonImporter.Read();

            var count = 1;
            foreach (var country in listOfCountries)
            {
                if (count > 10)
                {
                    break;
                }

                if (!countriesInDb.Any(c => c.Name == country.Name))
                {
                    Console.WriteLine("Adding country - {0}", country.Name);
                    dp.Countries.Add(new Country { Name = country.Name });
                    count++;
                }
            }

            dp.UnitOfWork.Finished();
        }

        private static void SqliteStart()
        {
            var kernel = new StandardKernel(new ATPTennisStatModules());
            var dp = kernel.Get<SqliteDataProvider>();
            var logs = dp.Logs;
            logs.Add(new Log { Message = "proba123", TimeStamp = DateTime.Now });
            dp.UnitOfWork.Finished();

            var logsList = dp.Logs.GetAll();
            foreach (var log in logsList)
            {
                Console.WriteLine("ID {0}", log.Id.ToString());
                Console.WriteLine("Message {0}", log.Message.ToString());
                Console.WriteLine("Time {0}", log.TimeStamp.ToString());
                Console.WriteLine("---------------");
            }
        }

        private static void ConsoleEngineStart()
        {
            var kernel = new StandardKernel(new ATPTennisStatModules());
            var engine = kernel.Get<IEngine>();
            engine.Start();

            #region Seed Data saved for later!
            // test
            /* Seed Data 
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
            } */
            #endregion
        }

        private static void ExcelImporter()
        {
            var kernel = new StandardKernel(new ATPTennisStatModules());

            var excelImporter = kernel.Get<ExcelImporter>();
            excelImporter.ImportPlayers();
            excelImporter.ImportTournaments();
            excelImporter.ImportPointDistributions();
            excelImporter.ImportMatches();


        }

        private static void GeneratePdfReport()
        {
            var kernel = new StandardKernel(new ATPTennisStatModules());
            var reportType = new Ninject.Parameters.ConstructorArgument("reportType", PdfReportType.Ranking);
            var report = kernel.Get<PdfReportGenerator>(reportType);
            report.GenerateReport();
        }

        private static void NinjectStart()
        {
            var kernel = new StandardKernel(new ATPTennisStatModules());
            var dp = kernel.Get<SqlServerDataProvider>();
            //var cities = dp.Cities.Find(c => c.Country.Name == "Bulgaria");

            var players = dp.Players.GetAll();
            //dp.Players.Add()
            
            dp.Cities.Add(new City
            {
                Name = "Burgas",
                Country = new Models.Country { Name = "Bulgaria" }
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
