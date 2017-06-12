using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ATPTennisStat.Models.SqlServerModels;
using ATPTennisStat.SQLServerData;

namespace ATPTennisStat.Importers
{
    public class JSONImporter
    {
        private const string jsonPath = "\\Data\\Json\\";
        private const string jsonFileName = "countries.json";
        private readonly string baseDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        private string fullPath;

        private ISqlServerDataProvider dataProvider;

        public JSONImporter(ISqlServerDataProvider dataProvider)
        {
            if (dataProvider == null)
            {
                throw new ArgumentNullException("SqlServerDataProvider");
            }

            this.fullPath = baseDir + jsonPath + jsonFileName;
            this.dataProvider = dataProvider;
        }

        public string FullPath
        {
            get
            {
                return this.fullPath;
            }

            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Json file path");
                }

                this.fullPath = value;
            }
        }

        public IEnumerable<Country> ReadFromFile()
        {
            var file = File.ReadAllText(fullPath);

            var countries = JsonConvert.DeserializeObject<List<Country>>(file);

            return countries;
        }

        public void WriteToDb()
        {
            var countriesInDb = this.dataProvider.Countries.GetAll();
            var countriesInJson = this.ReadFromFile();

            foreach (var country in countriesInJson)
            {
                if (!countriesInDb.Any(c => c.Name == country.Name))
                {
                    this.dataProvider.Countries.Add(new Country { Name = country.Name });
                }
            }

            this.dataProvider.UnitOfWork.Finished();
        }
    }
}