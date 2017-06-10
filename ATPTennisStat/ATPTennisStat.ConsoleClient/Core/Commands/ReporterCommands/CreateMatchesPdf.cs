using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.SQLServerData;
using System;
using System.Collections.Generic;

namespace ATPTennisStat.ConsoleClient.Core.Commands.ReporterCommands
{
    class CreateMatchesPdf : ICommand
    {
        private SqlServerDataProvider dp;
        private IWriter writer;

        public CreateMatchesPdf(SqlServerDataProvider sqlDP, IWriter writer)
        {
            this.dp = sqlDP;
            this.writer = writer;
        }

        public string Execute(IList<string> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
