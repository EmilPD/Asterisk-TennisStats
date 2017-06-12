using ATPTennisStat.Importers.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ATPTennisStat.Importers.ImportModels
{
    public class TournamentExcelImportModel: ITournamentExcelImportModel
    {
        public string Name { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string PrizeMoney { get; set; }

        public string Category { get; set; }

        public string PlayersCount { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Surface { get; set; }

        public string SurfaceSpeed { get; set; }
    }
}
