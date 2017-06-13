using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.ConsoleClient.Core.Utilities;
using ATPTennisStat.SQLiteData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataShowCommands
{
    public class ShowDetailedLogsCommand : ICommand
    {
        private ISqliteDataProvider dp;
        private IWriter writer;

        public ShowDetailedLogsCommand(ISqliteDataProvider sqliteDP, IWriter writer)
        {
            this.dp = sqliteDP;
            this.writer = writer;
        }

        public string Execute()
        {
            return $@"Not enough parameters!
Use this template [ld (logID)] and try again!";
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count < 1)
            {
                return this.Execute();
            }
            else
            {
                int id = -1;
                bool parsed = int.TryParse(parameters[0], out id);

                if (!parsed || id < 1)
                {
                    throw new ArgumentException("Provided Id is not a valid positive integer number!");
                }

                var log = this.dp.Logs.Get(id);

                if (log == null)
                {
                    throw new ArgumentException("Log with such id does not exist");
                }

                var result = new StringBuilder();
                result.AppendLine($"Log ID: {log.Id}");
                result.AppendLine($"Log Message: {log.Message}");
                result.AppendLine($"Log Timestamp: {log.TimeStamp}");

                var listOfLogDetails = this.dp.LogDetails.GetAllQuerable()
                                   .Where(ld => ld.LogId == id)
                                   .ToList();

                if (listOfLogDetails.Count > 0)
                {
                    foreach (var logDetail in listOfLogDetails)
                    {
                        result.Append($"Log-{logDetail.Id} -- ");
                        result.AppendLine($"{logDetail.Message} -- ");
                    }
                }
                else
                {
                    result.AppendLine($"No detailed logs available for this log");
                }


                result.AppendLine("");
                result.AppendLine("[menu]");
                return result.ToString();
            }

        }
    }
}
