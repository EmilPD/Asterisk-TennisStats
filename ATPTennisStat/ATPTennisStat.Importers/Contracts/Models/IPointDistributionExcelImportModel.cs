using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.Importers.Contracts.Models
{
    public interface IPointDistributionExcelImportModel
    {
        string Category { get; set; }

        string PlayersNumber { get; set; }

        string RoundName { get; set; }

        string Points { get; set; }
    }
}
