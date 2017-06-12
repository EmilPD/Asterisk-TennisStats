using System.Collections.Generic;
using System.Text;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.SQLiteData;

namespace ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataShowCommands
{
    class ShowLogsCommand : ICommand
    {
        private ISqliteDataProvider dp;
        private IWriter writer;

        public ShowLogsCommand(ISqliteDataProvider sqliteDP, IWriter writer)
        {
            this.dp = sqliteDP;
            this.writer = writer;
        }

        public string Execute(IList<string> parameters)
        {
            this.writer.Clear();
            var result = new StringBuilder();

            var listOfLogs = this.dp.Logs.GetAll();

            foreach (var log in listOfLogs)
            {
                result.Append($"Log-{log.Id} -- ");
                result.Append($"{log.Message} -- ");
                result.AppendLine($"{log.TimeStamp}");
            }

            result.AppendLine("");
            result.AppendLine("[menu]");
            return result.ToString();
        }
    }
}