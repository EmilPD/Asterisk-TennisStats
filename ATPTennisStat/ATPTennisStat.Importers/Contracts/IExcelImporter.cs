using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.Importers.Contracts
{
    public interface IExcelImporter
    {
        void ImportPointDistributions();

        void ImportPlayers();

        void ImportTournaments();

        void ImportMatches();

    }
}
