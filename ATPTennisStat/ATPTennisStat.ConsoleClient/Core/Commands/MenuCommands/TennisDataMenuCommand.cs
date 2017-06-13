using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.ConsoleClient.Core.Utilities;
using System;
using System.Collections.Generic;

namespace ATPTennisStat.ConsoleClient.Core.Commands.MenuCommands
{
    class TennisDataMenuCommand : ICommand
    {
        private IWriter writer;

        public TennisDataMenuCommand(IWriter writer)
        {
            this.writer = writer;
        }

        public string Execute()
        {
            this.writer.Clear();
            return
                Messages.GenerateWelcomeMessage() +
                Messages.GenerateDataMenu();
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count == 0)
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
