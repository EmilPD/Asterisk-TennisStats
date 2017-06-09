using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.IO;
using ATPTennisStat.Importers.Contracts;
using ATPTennisStat.SQLServerData;
using ATPTennisStat.Models;
using ATPTennisStat.Factories;
using System.Data;

namespace ATPTennisStat.Importers
{
    public class ExcelImporter : IImporter
    {
        private readonly string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        private string playersFilePath;
        private string matchesFilePath;

        private SqlServerDataProvider dataProvider;
        private ModelsFactory modelsFactory;

        public ExcelImporter(SqlServerDataProvider dataProvider, ModelsFactory modelsFactory)
        {
            this.dataProvider = dataProvider;
            this.modelsFactory = modelsFactory;


            this.playersFilePath = this.solutionDirectory + "\\Data\\Excel\\Players-Full-Data.xlsx";
            this.matchesFilePath = this.solutionDirectory + "\\Data\\Excel\\Matches-Full-Data.xlsx";
        }

        /// <summary>
        /// Expects a file with data in the first worksheet
        /// </summary>
        public IXLTableRange GenerateTableRangeFromFile(string filePath)
        {
            var workbook = new XLWorkbook(filePath);
            var ws = workbook.Worksheets.First();

            var dataRange = ws.RangeUsed().AsTable().DataRange;

            return dataRange;
        }

        public void ImportMatches()
        {
            var dataRange = GenerateTableRangeFromFile(this.matchesFilePath);
            var matches = dataRange.Rows()
                       .Select(row => new
                       {
                           DataPlayed = row.Field("DatePlayed").GetString(),
                           Winner = row.Field("Winner").GetString(),
                           Loser = row.Field("Loser").GetString(),
                           Result = row.Field("Result").GetString(),
                           WinnerPoints = row.Field("Winner Points").GetString(),
                           LoserPoints = row.Field("Loser Points").GetString(),
                           TournamentName = row.Field("Tournament").GetString(),
                           StartDate = row.Field("StartDate").GetString(),
                           EndDate = row.Field("EndDate").GetString(),
                           PrizeMoney = row.Field("PrizeMoney").GetString(),
                           TournamentCategory = row.Field("Category").GetString(),
                           PlayersCount = row.Field("PlayersCount").GetString(),
                           Round = row.Field("Round").GetString(),
                           City = row.Field("City").GetString(),
                           Surface = row.Field("Surface").GetString(),
                           Speed = row.Field("Speed").GetString()
                       })
                        .ToList();
            var a = 3;
        }

        public void ImportPlayers()
        {
            var dataRange = GenerateTableRangeFromFile(this.playersFilePath);

            var players = dataRange.Rows()
                .Select(row => new
                {
                    FirstName = row.Field("FirstName").GetString(),
                    LastName = row.Field("LastName").GetString(),
                    Ranking = row.Field("Ranking").GetString(),
                    BirthDate = row.Field("BirthDate").GetString(),
                    Height = row.Field("Height").GetString(),
                    Weight = row.Field("Weight").GetString(),
                    City = row.Field("City").GetString(),
                    Country = row.Field("Country").GetString()

                })
                .ToList();


            foreach (var p in players)
            {
                try
                {
                    var newPlayer = modelsFactory.CreatePlayer(
                     p.FirstName,
                     p.LastName,
                     p.Ranking,
                     p.BirthDate,
                     p.Height,
                     p.Weight,
                     p.City,
                     p.Country);

                    this.dataProvider.Players.Add(newPlayer);

                }
                catch (ArgumentException ex)
                {

                    Console.WriteLine("Excel import problem: " + ex.Message);
                }

            }

            this.dataProvider.UnitOfWork.Finished();
        }

        public void Write()
        {




            //var city = this.modelsFactory.CreateCity("Paris", "France");
            //this.dataProvider.Cities.Add(city);

            ////this.dataProvider.UnitOfWork.Finished();

            //var city1 = modelsFactory.CreateCity("Nant", "France");


            //this.dataProvider.Cities.Add(city1);

            //this.dataProvider.UnitOfWork.Finished();

        }


    }
}
