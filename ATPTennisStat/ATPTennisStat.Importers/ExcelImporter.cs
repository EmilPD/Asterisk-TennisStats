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
        private string solutionDirectory;
        private string filePath;

        private SqlServerDataProvider dataProvider;
        private ModelsFactory modelsFactory;

        public ExcelImporter(SqlServerDataProvider dataProvider, ModelsFactory modelsFactory)
        {
            this.dataProvider = dataProvider;
            this.modelsFactory = modelsFactory;

            this.solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            this.filePath = this.solutionDirectory + "\\Data\\Excel\\Players-Full-Data.xlsx";

        }

        /// <summary>
        /// Expects a file with data in the first worksheet
        /// </summary>
        public void Read()
        {
            var workbook = new XLWorkbook(this.filePath);
            var ws = workbook.Worksheets.First();

            var dataRange = ws.RangeUsed().AsTable().DataRange;


            var players = dataRange.Rows()
                .Select(nameRow => new {
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

            this.dataProvider.UnitOfWork.Finished();
        }

        public void ImportPlayer()
        {

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
