using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using ATPTennisStat.Models;

namespace ATPTennisStat.Importers
{
    public class JSONImporter
    {
        private string path;

        public JSONImporter(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("json path");
            }

            this.path = path;
        }

        public IEnumerable<Country> Read()
        {
            
            var file = File.ReadAllText(path);

            var countries = JsonConvert.DeserializeObject<List<Country>>(file);

            return countries;
        }

    }
}
