using ATPTennisStat.ConsoleClient.Core.Commands.Contracts;
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
    public class ImportMatchesCommand : ICommandNoParameters
    {
        private ISqlServerDataProvider dataProvider;
        private IModelsFactory modelsFactory;
        private IExcelImporter excelImporter;
        private IWriter writer;
        private ILogger logger;

        public ImportMatchesCommand(ISqlServerDataProvider dataProvider,
                            IModelsFactory modelsFactory,
                            IExcelImporter excelImporter,
                            IWriter writer,
                            ILogger logger)
        {
            this.dataProvider = dataProvider;
            this.modelsFactory = modelsFactory;
            this.excelImporter = excelImporter;
            this.writer = writer;
            this.logger = logger;

        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count == 0)
            {
                return this.Execute();
            }
            else
            {
                return "This command takes no parameters";
            }
        }

        public string Execute()
        {
            var matches = excelImporter.ImportMatches();

            writer.WriteLine("Total records in dataset: " + matches.Count);
            var counterAdded = 0;
            var counterDuplicates = 0;

            writer.Write("Importing matches' data...");

            foreach (var m in matches)
            {
                try
                {
                    var newMatch = modelsFactory.CreateMatch(
                         m.DatePlayed,
                         m.Winner,
                         m.Loser,
                         m.Result,
                         m.TournamentName,
                         m.Round
                     );



                    this.dataProvider.Matches.Add(newMatch);
                    counterAdded++;
                }
                catch (ArgumentException ex)
                {
                    //log(("Excel import problem: " + ex.Message)) PSEUDO CODE
                    counterDuplicates++;
                }

            }

            this.dataProvider.UnitOfWork.Finished();
            writer.Write(Environment.NewLine);

            return String.Format("Records added: {0}{1}Duplicated records: {2}", counterAdded, Environment.NewLine, counterDuplicates);
        }
    }
}
