using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Factories.Contracts;
using ATPTennisStat.SQLServerData;
using System;
using System.Collections.Generic;

namespace ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataShowCommands
{
    class AddMatchCommand : ICommand
    {
        private SqlServerDataProvider dp;
        private IWriter writer;
        private IModelsFactory factory;

        public AddMatchCommand(SqlServerDataProvider sqlDP, IWriter writer, IModelsFactory factory)
        {
            this.dp = sqlDP;
            this.writer = writer;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            throw new NotImplementedException();
        }
    }
}