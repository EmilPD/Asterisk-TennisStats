using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.ConsoleClient.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.ConsoleClient.Core.Commands.MenuCommands
{
    class TeamInfoCommand : ICommand
    {
        private IWriter writer;

        public TeamInfoCommand(IWriter writer)
        {
            this.writer = writer;
        }

        public string Execute(IList<string> parameters)
        {
            this.writer.Clear();
            var result = new StringBuilder();
            result.AppendLine(Messages.GenerateWelcomeMessage());
            result.AppendLine("Emil - qwerty123");
            result.AppendLine("Ivan - tinmanjk");
            result.AppendLine("Zach - ZachD");
            result.AppendLine("[menu]");
            return result.ToString();
        }
    }
}
