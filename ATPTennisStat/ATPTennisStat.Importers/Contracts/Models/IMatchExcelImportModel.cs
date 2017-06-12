using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.Importers.Contracts.Models
{
    public interface IMatchExcelImportModel
    {
        string DatePlayed { get; set; }

        string Winner { get; set; }

        string Loser { get; set; }

        string Result { get; set; }

        string TournamentName { get; set; }

        string Round { get; set; }

        string City { get; set; }

        string Country { get; set; }
    }
}
