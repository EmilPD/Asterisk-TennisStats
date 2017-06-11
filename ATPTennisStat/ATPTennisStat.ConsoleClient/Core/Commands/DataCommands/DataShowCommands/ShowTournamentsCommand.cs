using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Models;
using ATPTennisStat.SQLServerData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataShowCommands
{
    class ShowTournamentsCommand : ICommand
    {
        private ISqlServerDataProvider dp;
        private IWriter writer;

        public ShowTournamentsCommand(ISqlServerDataProvider sqlDP, IWriter writer)
        {
            this.dp = sqlDP;
            this.writer = writer;
        }

        public string Execute(IList<string> parameters)
        {
            this.writer.Clear();
            var result = new StringBuilder();
            var tournaments = dp.Tournaments.GetAll();

            foreach (Tournament evt in tournaments)
            {
                int i = 0;
                var matchesCount = dp
                    .Matches
                    .GetAll()
                    .Where((t => t.Tournament.Id == evt.Id))
                    .ToList().Count;

                if (parameters.Count > 0)
                {
                    int tournId = -1;
                    int.TryParse(parameters[0], out tournId);
                    if (tournId > 0)
                    {
                        if (evt.Id == tournId)
                        {
                            result.AppendLine($"* {evt.Name} *");
                            result.AppendLine(new string('-', evt.Name.Length + 4));
                            result.AppendLine($"    Id: {tournId}");
                            result.AppendLine($"    Start: {evt.StartDate}");
                            result.AppendLine($"    Finish: {evt.EndDate}");
                            result.AppendLine($"    Category: {evt.Category.Category}, {evt.Category.PlayersCount} players");
                            result.AppendLine($"    Total prize money: ${evt.PrizeMoney}");
                            result.AppendLine($"    Surface: {evt.Type.Type}, {evt.Type.Speed}");
                            result.AppendLine($"    City: {evt.City.Name}, {evt.City.Country.Name}");
                            result.AppendLine($"    Matches played: {matchesCount}");
                            result.AppendLine("");
                        }
                        else
                        {
                            if (i < tournaments.Count())
                            {
                                continue;
                            }
                            else
                            {
                                throw new ArgumentException($"Sorry, no tournament with id {tournId} exists!");
                            }
                        }

                        i++;
                    }
                    else
                    {
                        throw new ArgumentException($"Sorry, {tournId} is not a valid number!");
                    }
                }
                else
                {
                    result.AppendLine($"Id: {evt.Id} | Name: {evt.Name} | Matches Played: {matchesCount}");
                }
            }
            result.AppendLine("");
            result.AppendLine("[menu] [show] [showt] [showp]");
            return result.ToString();
        }
    }
}