using System.Collections.Generic;
using System.Text;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.SQLiteData;
using System.Linq;
using System;

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

        public string Execute()
        {
            this.writer.Clear();
            var result = new StringBuilder();

            var listOfLogs = this.dp.Logs.GetAllQuerable().ToList();

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
        public string Execute(IList<string> parameters)
        {
            if (parameters.Count == 0)
            {
                return this.Execute();
            }
            else
            {
                throw new ArgumentException("This command does not take in any parameters");
            }
        }
    }
}