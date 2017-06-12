using ATPTennisStat.Importers.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.Importers.ImportModels
{
    public class MatchExcelImportModel : IMatchExcelImportModel
    {
        public string DatePlayed { get; set; }

        public string Winner { get; set; }

        public string Loser { get; set; }

        public string Result { get; set; }

        public string TournamentName { get; set; }

        public string Round { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
