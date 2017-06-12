using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Factories.Contracts;
using ATPTennisStat.Importers.Contracts;
using ATPTennisStat.SQLServerData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.ConsoleClient.Core.Commands.ImportCommands
{
    public class ImportSampleDataCommand : ICommand
    {
        private ISqlServerDataProvider dataProvider;
        private IModelsFactory modelsFactory;
        private IExcelImporter excelImporter;
        private IWriter writer;
        private ILogger logger;
        private ICommandFactory commandsFactory;

        public ImportSampleDataCommand(ISqlServerDataProvider dataProvider,
                                       IModelsFactory modelsFactory,
                                       IExcelImporter excelImporter,
                                       IWriter writer,
                                       ILogger logger,
                                       ICommandFactory commandsFactory)
        {
            this.dataProvider = dataProvider;
            this.modelsFactory = modelsFactory;
            this.excelImporter = excelImporter;
            this.writer = writer;
            this.logger = logger;
            this.commandsFactory = commandsFactory;

        }

        public string Execute(IList<string> parameters)
        {
            
            var importPlayersResult = commandsFactory.ImportPlayers().Execute(new List<string>());
            writer.WriteLine(importPlayersResult);

            var importPointDistributionsResult = commandsFactory.ImportPointDistributions().Execute(new List<string>());
            writer.WriteLine(importPointDistributionsResult);

            var importTournamentsResult = commandsFactory.ImportTournaments().Execute(new List<string>());
            writer.WriteLine(importTournamentsResult);

            var importMatchesResult = commandsFactory.ImportMatches().Execute(new List<string>());
            writer.WriteLine(importMatchesResult);

            return "Successfully (re-)added sample data";
        }
    }
}
