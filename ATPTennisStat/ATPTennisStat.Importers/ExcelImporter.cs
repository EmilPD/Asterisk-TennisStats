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
            var workbook = new XLWorkbook(this.playersFilePath);
            var ws = workbook.Worksheets.First();

            var dataRange = ws.RangeUsed().AsTable().DataRange;

            return dataRange;
        }

        public void ImportPlayers()
        {
            var dataRange = GenerateTableRangeFromFile(this.playersFilePath);

            var players = dataRange.Rows()
                .Select(nameRow => new
                {
                    FirstName = nameRow.Field("FirstName").GetString(),
                    LastName = nameRow.Field("LastName").GetString(),
                    Ranking = nameRow.Field("Ranking").GetString(),
                    BirthDate = nameRow.Field("BirthDate").GetString(),
                    Height = nameRow.Field("Height").GetString(),
                    Weight = nameRow.Field("Weight").GetString(),
                    City = nameRow.Field("City").GetString(),
                    Country = nameRow.Field("Country").GetString()

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
