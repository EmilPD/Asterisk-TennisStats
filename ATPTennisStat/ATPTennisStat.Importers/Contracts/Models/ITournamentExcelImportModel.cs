using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.Importers.Contracts.Models
{
    public interface ITournamentExcelImportModel
    {
        string Name { get; set; }

        string StartDate { get; set; }

        string EndDate { get; set; }

        string PrizeMoney { get; set; }

        string Category { get; set; }

        string PlayersCount { get; set; }

        string City { get; set; }

        string Country { get; set; }

        string Surface { get; set; }

        string SurfaceSpeed { get; set; }
    }
}
