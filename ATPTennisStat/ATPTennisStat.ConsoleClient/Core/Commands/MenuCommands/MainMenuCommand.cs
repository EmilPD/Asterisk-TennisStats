using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.ConsoleClient.Core.Utilities;
using System.Collections.Generic;

namespace ATPTennisStat.ConsoleClient.Core.Commands.MenuCommands
{
    class MainMenuCommand : ICommand
    {
        private IWriter writer;

        public MainMenuCommand(IWriter writer)
        {
            this.writer = writer;
        }

        public string Execute(IList<string> parameters)
        {
            this.writer.Clear();
            return 
                Messages.GenerateWelcomeMessage() +
                Messages.GenerateMainMenu();
        }
    }
}
