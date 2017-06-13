using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.IO;
using ATPTennisStat.Importers.Contracts;
using ATPTennisStat.SQLServerData;
using ATPTennisStat.Models.SqlServerModels;
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
        private const string errorMessageInvalidHeders = "Invalid Data Error: Data should be formatted having the following headers in the first row of the worksheet:" +
                                                    "\r\n";
        private const string errorMessageNoDataInFirstSheet = "No data in the first sheet of the file";

        private ISqlServerDataProvider dataProvider;
        private IModelsFactory modelsFactory;

        public ExcelImporter(ISqlServerDataProvider dataProvider, IModelsFactory modelsFactory)
        {
            this.dataProvider = dataProvider;
            this.modelsFactory = modelsFactory;


            this.pointDistributionsFilePath = this.solutionDirectory + "\\Data\\Excel\\Sample Data\\TournamentCategoryPoints.xlsx";
            this.playersFilePath = this.solutionDirectory + "\\Data\\Excel\\Sample Data\\players-2016.xlsx";
            this.matchesFilePath = this.solutionDirectory + "\\Data\\Excel\\Sample Data\\matches-2016.xlsx";
            this.tournamentsFilePath = this.solutionDirectory + "\\Data\\Excel\\Sample Data\\tournaments-2016.xlsx";
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
                throw new ArgumentException(ex.Message);
            }
        }

        public IList<IPointDistributionExcelImportModel> ImportPointDistributions(string filePath)
        {
            if (filePath == "sampleDataImport")
            {
                filePath = this.pointDistributionsFilePath;
            }

            var dataRange = GenerateTableRangeFromFile(filePath);

            if (dataRange == null)
            {
                throw new ArgumentException(errorMessageNoDataInFirstSheet);
            }

            try
            {
                var pointDistributions = dataRange.Rows()
                    .Select(row => new PointDistributionExcelImportModel
                    {
                        Category = row.Field("Category").GetString().Trim(),
                        PlayersNumber = row.Field("PlayersNumber").GetString().Trim(),
                        RoundName = row.Field("Round Name").GetString().Trim(),
                        Points = row.Field("Points").GetString().Trim()
                    })
                    .ToList<IPointDistributionExcelImportModel>();
                return pointDistributions;

            }
            catch (Exception)
            {
                throw new ArgumentException(errorMessageInvalidHeders +
                    "Category | PlayersNumber | Round Name| Points");
            }
        }

        public IList<ITournamentExcelImportModel> ImportTournaments(string filePath)
        {

            if (filePath == "sampleDataImport")
            {
                filePath = this.tournamentsFilePath;
            }

            var dataRange = GenerateTableRangeFromFile(filePath);

            if (dataRange == null)
            {
                throw new ArgumentException(errorMessageNoDataInFirstSheet);
            }

            try
            {
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
            catch (Exception)
            {
                throw new ArgumentException(errorMessageInvalidHeders +
                    "Name | StartDate | EndDate| PrizeMoney | Category| PlayersCount | City | Country| Surface | Speed");
            }
        }

        public IList<IMatchExcelImportModel> ImportMatches(string filePath)
        {
            if (filePath == "sampleDataImport")
            {
                filePath = this.matchesFilePath;
            }

            var dataRange = GenerateTableRangeFromFile(filePath);

            if (dataRange == null)
            {
                throw new ArgumentException(errorMessageNoDataInFirstSheet);
            }

            try
            {
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
            catch (Exception)
            {
                throw new ArgumentException(errorMessageInvalidHeders +
                                   "DatePlayed | Winner | Loser| Result | Tournament| Round");
            }
        }

        public IList<IPlayerExcelImportModel> ImportPlayers(string filePath)
        {
            if(filePath == "sampleDataImport")
            {
                filePath = this.playersFilePath;
            }

            var dataRange = GenerateTableRangeFromFile(filePath);

            if (dataRange == null)
            {
                throw new ArgumentException(errorMessageNoDataInFirstSheet);
            }

            try
            {
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
            catch (Exception)
            {
                throw new ArgumentException(errorMessageInvalidHeders +
                                   "FirstName | LastName | Ranking| BirthDate | Height| Weight | City| Country");
            }
        }
    }
}
