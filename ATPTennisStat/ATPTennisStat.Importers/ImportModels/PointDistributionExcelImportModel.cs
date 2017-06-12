using ATPTennisStat.Importers.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.Importers.ImportModels
{
    public class PointDistributionExcelImportModel : IPointDistributionExcelImportModel
    {
        public string Category { get; set; }

        public string PlayersNumber { get; set; }

        public string RoundName { get; set; }

        public string Points { get; set; }
    }
}
