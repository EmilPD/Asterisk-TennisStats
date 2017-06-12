using ATPTennisStat.Importers.Contracts;
using ATPTennisStat.Importers.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.Importers.ImportModels
{
    public class PlayerExcelImportModel : IPlayerExcelImportModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Ranking { get; set; }

        public string Birthdate { get; set; }

        public string Height { get; set; }

        public string Weight { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

    }
}
