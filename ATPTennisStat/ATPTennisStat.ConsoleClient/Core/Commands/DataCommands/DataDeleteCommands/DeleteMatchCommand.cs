using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.SQLServerData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataDeleteCommands
{
    class DeleteMatchCommand : ICommand
    {
        private ISqlServerDataProvider dp;
        private IReader reader;
        private IWriter writer;

        public DeleteMatchCommand(ISqlServerDataProvider sqlDP, IReader reader, IWriter writer)
        {
            this.dp = sqlDP;
            this.reader = reader;
            this.writer = writer;
        }

        public string Execute(IList<string> parameters)
        {
            writer.Clear();

            if (parameters.Count == 1)
            {
                int id = -1;
                bool parsed = int.TryParse(parameters[0], out id);
                if (parsed)
                {
                    var match = dp.Matches.Get(id);
                    if (match != null)
                    {
                        this.writer.WriteLine("Are you sure? Y/N");
                        var answer = this.reader.ReadLine();
                        if (answer.ToLower() == "y")
                        {
                            dp.Matches.Remove(match);
                            dp.UnitOfWork.Finished();
                            return $"Match with Id - {id} was removed successfully!";
                        }
                        else
                        {
                            return "Delete terminated! Match is not deleted!";
                        }
                    }
                    else
                    {
                        throw new NullReferenceException("Match to remove cannot be null!");
                    }
                }
                else
                {
                    throw new NullReferenceException("Index cannot be parsed!");
                }
            }
            return $@"Not enough parameters!
Use this template [delm (id)] and try again!
Id must be a valid number!";
        }
    }
}
