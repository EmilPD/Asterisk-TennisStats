using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.Importers.Contracts.Models
{
    public interface IPlayerExcelImportModel
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        string Ranking { get; set; }

        string Birthdate { get; set; }

        string Height { get; set; }

        string Weight { get; set; }

        string City { get; set; }

        string Country { get; set; }
    }
}
