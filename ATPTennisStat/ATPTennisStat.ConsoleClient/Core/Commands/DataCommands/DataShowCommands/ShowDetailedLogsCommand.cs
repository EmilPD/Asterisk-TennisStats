using ATPTennisStat.ConsoleClient.Core.Contracts;
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

        public string Execute(IList<string> parameters)
        {

            this.writer.Clear();
            var result = new StringBuilder();

            var listOfLogs = this.dp.LogDetails.GetAllQuerable()
                               .Where(ld => ld.LogId == 11)
                               .ToList();

            foreach (var log in listOfLogs)
            {
                result.Append($"Log-{log.Id} -- ");
                result.AppendLine($"{log.Message} -- ");
                //result.AppendLine($"{log.TimeStamp}");
            }

            result.AppendLine("");
            result.AppendLine("[menu]");
            return result.ToString();
        }
    }
}
