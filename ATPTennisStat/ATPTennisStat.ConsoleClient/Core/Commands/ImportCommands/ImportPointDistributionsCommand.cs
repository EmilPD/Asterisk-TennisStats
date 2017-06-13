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
    public class ImportPointDistributionsCommand : ICommand
    {
        private ISqlServerDataProvider dataProvider;
        private IModelsFactory modelsFactory;
        private IExcelImporter excelImporter;
        private IWriter writer;
        private ILogger logger;

        public ImportPointDistributionsCommand(ISqlServerDataProvider dataProvider,
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


        public string Execute()
        {
            return $@"Not enough parameters!
Use this template [importpd FILE_PATH] and try again!";
        }

        public string Execute(IList<string> parameters)
        {

            if (parameters.Count == 0)
            {
                return this.Execute();
            }
            else
            {
                var pointDistributions = excelImporter.ImportPointDistributions(parameters[0]);

                writer.WriteLine("Total records in dataset: " + pointDistributions.Count);

                var counterAdded = 0;
                var counterDuplicates = 0;

                writer.Write("Importing point distributions' data...");

                var newLog = logger.CreateNewLog("Point distributions import: ");

                foreach (var pd in pointDistributions)
                {
                    try
                    {
                        var newPointDistribution = modelsFactory.CreatePointDistribution(
                         pd.Category,
                         pd.PlayersNumber,
                         pd.RoundName,
                         pd.Points);

                        this.dataProvider.PointDistributions.Add(newPointDistribution);
                        counterAdded++;

                    }
                    catch (ArgumentException ex)
                    {
                        var logDetail = logger.CreateNewLogDetail(
                                   "Excel import problem: " + ex.Message,
                                   newLog);

                        counterDuplicates++;
                    }

                }

                this.dataProvider.UnitOfWork.Finished();
                writer.Write(Environment.NewLine);

                newLog.TimeStamp = DateTime.Now;
                newLog.Message = newLog.Message + String.Format("Records added: {0}, Duplicated records: {1}", counterAdded, counterDuplicates);
                logger.Log(newLog);
                return String.Format("Records added: {0}{1}Duplicated records: {2}", counterAdded, Environment.NewLine, counterDuplicates);
            }


        }
    }
}
