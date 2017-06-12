using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.ConsoleClient.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.ConsoleClient.Core.Commands.MenuCommands
{
    public class ReportersMenuCommand : ICommand
    {
        private IWriter writer;

        public ReportersMenuCommand(IWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("Writer cannot be null!");
            }

            this.writer = writer;
        }

        public string Execute(IList<string> parameters)
        {
            this.writer.Clear();
            return
                Messages.GenerateWelcomeMessage() +
                Messages.GenerateReportersMenu();
        }
    }
}
