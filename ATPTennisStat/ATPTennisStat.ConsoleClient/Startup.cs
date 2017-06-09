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

            ///<summary>
            ///Control Flow -> choose either of the following methods
            ///</summary>
            //DbContextStart();
            //ExcelImporter();
            //NinjectStart();
            //GeneratePdfReport();
            //ConsoleEngineStart();
            SqliteStart();
        }

        private static void SqliteStart()
        {
            var kernel = new StandardKernel(new ATPTennisStatModules(DbContextType.SQLite));
            var dp = kernel.Get<SqliteDataProvider>();
            
            var log = new Log();
            log.Message = "Content2";

            dp.Logs.Add(log);
            dp.UnitOfWork.Finished();
        }

        private static void ConsoleEngineStart()
        {
            var kernel = new StandardKernel(new ATPTennisStatModules(DbContextType.Postgre));
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
            var kernel = new StandardKernel(new ATPTennisStatModules(DbContextType.SQLServer));

            var excelImporter = kernel.Get<ExcelImporter>();
            excelImporter.Read();
            //excelImporter.Write();

        }

        private static void GeneratePdfReport()
        {
            var kernel = new StandardKernel(new ATPTennisStatModules(DbContextType.SQLServer));
            var reportType = new Ninject.Parameters.ConstructorArgument("reportType", PdfReportType.Ranking);
            var report = kernel.Get<PdfReportGenerator>(reportType);
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
