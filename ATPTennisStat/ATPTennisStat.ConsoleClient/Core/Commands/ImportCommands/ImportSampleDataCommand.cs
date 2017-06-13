using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.ConsoleClient.Core.Utilities;
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

        public string Execute()
        {
            var importPlayersResult = commandsFactory.ImportPlayers().Execute();
            writer.WriteLine(importPlayersResult);
            writer.WriteLine("------------------");
            var importPointDistributionsResult = commandsFactory.ImportPointDistributions().Execute();
            writer.WriteLine(importPointDistributionsResult);
            writer.WriteLine("------------------");

            var importTournamentsResult = commandsFactory.ImportTournaments().Execute();
            writer.WriteLine(importTournamentsResult);
            writer.WriteLine("------------------");

            var importMatchesResult = commandsFactory.ImportMatches().Execute();
            writer.WriteLine(importMatchesResult);
            writer.WriteLine("------------------");

            return "Successfully (re-)added sample data";
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count==0)
            {
                return this.Execute();
            }
            else
            {
                throw new ArgumentException(Messages.ParametersWarning);
            }

        }
    }
}
