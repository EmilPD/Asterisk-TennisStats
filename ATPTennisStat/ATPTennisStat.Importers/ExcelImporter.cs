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
using ATPTennisStat.Factories.Contracts;
using System.Collections;
using ATPTennisStat.Importers.ImportModels;
using ATPTennisStat.Importers.Contracts.Models;

namespace ATPTennisStat.Importers
{
    public class ExcelImporter : IImporter, IExcelImporter
    {
        private readonly string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        private string playersFilePath;
        private string matchesFilePath;
        private string tournamentsFilePath;
        private string pointDistributionsFilePath;

        private ISqlServerDataProvider dataProvider;
        private IModelsFactory modelsFactory;

        public ExcelImporter(ISqlServerDataProvider dataProvider, IModelsFactory modelsFactory)
        {
            this.dataProvider = dataProvider;
            this.modelsFactory = modelsFactory;


            //this.playersFilePath = this.solutionDirectory + "\\Data\\Excel\\Players-Full-Data.xlsx";
            this.pointDistributionsFilePath = this.solutionDirectory + "\\Data\\Excel\\TournamentCategoryPoints.xlsx";

            this.playersFilePath = this.solutionDirectory + "\\Data\\Excel\\Big Data\\players-2016.xlsx";

            this.matchesFilePath = this.solutionDirectory + "\\Data\\Excel\\Big Data\\matches-2016.xlsx";
            this.tournamentsFilePath = this.solutionDirectory + "\\Data\\Excel\\Big Data\\tournaments-2016.xlsx";
        }

        /// <summary>
        /// Expects a file with data in the first worksheet
        /// </summary>
        private IXLTableRange GenerateTableRangeFromFile(string filePath)
        {
            try
            {
                var workbook = new XLWorkbook(filePath);
                var ws = workbook.Worksheets.First();

                var dataRange = ws.RangeUsed().AsTable().DataRange;

                return dataRange;
            }
            catch (Exception ex)
            {

                //throw new ArgumentException("File opened by another program");
                Console.WriteLine(ex.Message);
                return null;
            }


        }

        public void ImportPointDistributions()
        {
            var dataRange = GenerateTableRangeFromFile(this.pointDistributionsFilePath);

            if (dataRange == null)
            {
                //another exception handling possible
                return;
            }

            //TODO Exception Handling
            var pointDistributions = dataRange.Rows()
                            .Select(row => new
                            {
                                Category = row.Field("Category").GetString().Trim(),
                                PlayersNumber = row.Field("PlayersNumber").GetString().Trim(),
                                RoundName = row.Field("Round Name").GetString().Trim(),
                                Points = row.Field("Points").GetString().Trim()
                            })
                            .ToList();



            foreach (var pd in pointDistributions)
            {
                try
                {
                    var newPointDistribution = modelsFactory.CreatePointDistribution(
                     pd.Category,
                     pd.PlayersNumber,
                     pd.RoundName,
                     pd.Points);

                    this.dataProvider.PointDistributions.Add(newPointDistribution);

                }
                catch (ArgumentException ex)
                {

                    Console.WriteLine("Excel import problem: " + ex.Message);
                }

            }

            this.dataProvider.UnitOfWork.Finished();

        }

        public IList<ITournamentExcelImportModel> ImportTournaments()
        {
            var dataRange = GenerateTableRangeFromFile(this.tournamentsFilePath);

            if (dataRange == null)
            {
                throw new ArgumentException("No data in the first sheet of the file");
            }

            //TODO Exception Handling
            var tournaments = dataRange.Rows()
                            .Select(row => new TournamentExcelImportModel
                            {
                                Name = row.Field("Name").GetString().Trim(),
                                StartDate = row.Field("StartDate").GetString().Trim(),
                                EndDate = row.Field("EndDate").GetString().Trim(),
                                PrizeMoney = row.Field("PrizeMoney").GetString().Trim(),
                                Category = row.Field("Category").GetString().Trim(),
                                PlayersCount = row.Field("PlayersCount").GetString().Trim(),
                                City = row.Field("City").GetString().Trim(),
                                Country = row.Field("Country").GetString().Trim(),
                                Surface = row.Field("Surface").GetString().Trim(),
                                SurfaceSpeed = row.Field("Speed").GetString().Trim()
                            })
                            .ToList<ITournamentExcelImportModel>();

            return tournaments;
        }

        public IList<IMatchExcelImportModel> ImportMatches()
        {

            var dataRange = GenerateTableRangeFromFile(this.matchesFilePath);

            if (dataRange == null)
            {
                throw new ArgumentException("No data in the first sheet of the file");
            }

            var matches = dataRange.Rows()
                       .Select(row => new MatchExcelImportModel
                       {
                           DatePlayed = row.Field("DatePlayed").GetString().Trim(),
                           Winner = row.Field("Winner").GetString().Trim(),
                           Loser = row.Field("Loser").GetString().Trim(),
                           Result = row.Field("Result").GetString().Trim(),
                           TournamentName = row.Field("Tournament").GetString().Trim(),
                           Round = row.Field("Round").GetString().Trim()
                       })
                        .ToList<IMatchExcelImportModel>();
            return matches;
        }

        public IList<IPlayerExcelImportModel> ImportPlayers()
        {
            var dataRange = GenerateTableRangeFromFile(this.playersFilePath);

            if (dataRange == null)
            {
                throw new ArgumentException("No data in the first sheet of the file");
            }

            var players = dataRange.Rows()
                .Select(row => new PlayerExcelImportModel
                {
                    FirstName = row.Field("FirstName").GetString().Trim(),
                    LastName = row.Field("LastName").GetString().Trim(),
                    Ranking = row.Field("Ranking").GetString().Trim(),
                    Birthdate = row.Field("BirthDate").GetString().Trim(),
                    Height = row.Field("Height").GetString().Trim(),
                    Weight = row.Field("Weight").GetString().Trim(),
                    City = row.Field("City").GetString().Trim(),
                    Country = row.Field("Country").GetString().Trim()

                })
                .ToList<IPlayerExcelImportModel>();
            return players;
        }
    }
}
