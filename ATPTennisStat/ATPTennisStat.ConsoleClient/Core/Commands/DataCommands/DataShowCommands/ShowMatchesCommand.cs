using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Models.SqlServerModels;
using ATPTennisStat.SQLServerData;

namespace ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataShowCommands
{
    public class ShowMatchesCommand : ICommand
    {
        private ISqlServerDataProvider dp;
        private IWriter writer;

        public ShowMatchesCommand(ISqlServerDataProvider sqlDP, IWriter writer)
        {
            this.dp = sqlDP;
            this.writer = writer;
        }

        public string Execute()
        {
            this.writer.Clear();
            var result = new StringBuilder();
            var matches = dp.Matches.GetAll();

            foreach (var match in matches)
            {
                string winnerName = $"{match.Winner.FirstName} {match.Winner.LastName}";
                string loserName = $"{match.Loser.FirstName} {match.Loser.LastName}";

                result.AppendLine($"Id: {match.Id} | Date: {match.DatePlayed.ToString("d", DateTimeFormatInfo.InvariantInfo)} | {winnerName} : {loserName} | Result: {match.Result}");
            }

            result.AppendLine("");
            result.AppendLine("[menu] [show] [showt] [showp]");
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
                this.writer.Clear();
                var result = new StringBuilder();
                var matches = dp.Matches.GetAll();

                int matchId = -1;
                int.TryParse(parameters[0], out matchId);

                if (matchId > 0)
                {
                    Match match = dp.Matches.Get(matchId);

                    if (match != null)
                    {
                        string winnerName = $"{match.Winner.FirstName} {match.Winner.LastName}";
                        string loserName = $"{match.Loser.FirstName} {match.Loser.LastName}";

                        string fullName = $"{winnerName} : {loserName}";
                        result.AppendLine(fullName);
                        result.AppendLine(new string('-', fullName.Length + 4));
                        result.AppendLine($"    Result: {match.Result}");
                        result.AppendLine($"    Date: {match.DatePlayed.ToString("d", DateTimeFormatInfo.InvariantInfo)}");
                        result.AppendLine($"    Tournament: {match.Tournament.Name}");
                        result.AppendLine($"    Round: {match.Round.Stage}");
                        result.AppendLine($"    Surface: {match.Tournament.Type.Type}, {match.Tournament.Type.Speed}");
                        result.AppendLine($"    City: {match.Tournament.City.Name}, {match.Tournament.City.Country.Name}");
                    }
                    else
                    {
                        throw new ArgumentException($"Sorry, no match with id {matchId} exists!");
                    }
                }
                else
                {
                    throw new ArgumentException($"Sorry, {matchId} is not a valid number!");
                }

                result.AppendLine("");
                result.AppendLine("[menu] [show] [showt] [showp]");
                return result.ToString();
            }
        }
    }
}