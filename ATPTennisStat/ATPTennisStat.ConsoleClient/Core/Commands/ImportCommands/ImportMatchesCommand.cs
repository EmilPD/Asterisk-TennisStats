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
    public class ImportMatchesCommand : ICommand
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
            var matches = excelImporter.ImportMatches();
            writer.WriteLine(matches.Count);

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

                }
                catch (ArgumentException ex)
                {

                    writer.WriteLine("Excel import problem: " + ex.Message);
                }

            }

            this.dataProvider.UnitOfWork.Finished();

            return "ImportMatches";
        }
    }
}
