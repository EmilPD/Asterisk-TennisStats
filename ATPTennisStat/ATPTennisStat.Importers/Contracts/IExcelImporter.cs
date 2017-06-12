using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.Importers.Contracts
{
    public interface IExcelImporter
    {
        void ImportPointDistributions();

        IList<IPlayerExcelImportModel> ImportPlayers();

        void ImportTournaments();

        void ImportMatches();

    }
}
