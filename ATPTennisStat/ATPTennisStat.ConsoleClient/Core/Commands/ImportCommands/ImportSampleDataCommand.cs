using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Importers.Contracts;
using ATPTennisStat.SQLServerData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.ConsoleClient.Core.Commands.ImportCommands
{
    class ImportSampleDataCommand : ICommand
    {
        private ISqlServerDataProvider dp;
        private IExcelImporter excelImporter;
        private IReader reader;
        private IWriter writer;

        public ImportSampleDataCommand(ISqlServerDataProvider sqlDP, IExcelImporter excelImporter, IReader reader, IWriter writer)
        {
            this.dp = sqlDP;
            this.excelImporter = excelImporter;
            this.reader = reader;
            this.writer = writer;
        }

        public string Execute(IList<string> parameters)
        {
            excelImporter.ImportPlayers();
            excelImporter.ImportTournaments();
            excelImporter.ImportPointDistributions();
            excelImporter.ImportMatches();

            return "";
        }
    }
}
