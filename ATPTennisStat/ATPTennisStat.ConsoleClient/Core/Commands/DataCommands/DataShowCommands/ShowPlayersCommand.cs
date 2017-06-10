using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Models;
using ATPTennisStat.SQLServerData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataShowCommands
{
    class ShowPlayersCommand : ICommand
    {
        private SqlServerDataProvider dp;
        private IWriter writer;

        public ShowPlayersCommand(SqlServerDataProvider sqlDP, IWriter writer)
        {
            this.dp = sqlDP;
            this.writer = writer;
        }

        public string Execute(IList<string> parameters)
        {
            this.writer.Clear();
            var result = new StringBuilder();
            var players = dp.Players.GetAll();

            foreach (Player player in players)
            {
                int i = 0;
                var matchesCount = dp
                    .Matches
                    .Find((m => (m.Winner.Id == player.Id || m.Loser.Id == player.Id)))
                    .ToList().Count;

                if (parameters.Count > 0)
                {
                    int playerId = -1;
                    int.TryParse(parameters[0], out playerId);
                    if (playerId > 0)
                    {
                        if (player.Id == playerId)
                        {
                            string fullName = $"* {player.FirstName} {player.LastName} *";
                            result.AppendLine(fullName);
                            result.AppendLine(new string('-', fullName.Length + 4));
                            result.AppendLine($"    Id: {playerId}");

                            DateTime birthDay = DateTime.Now;
                            DateTime.TryParse(player.BirthDate.ToString(), out birthDay);
                            int playerAge = Convert.ToInt32(Math.Floor((DateTime.Now.Subtract(birthDay).TotalDays/360)));

                            if(playerAge > 1)
                            {
                                result.AppendLine($"    Age: {playerAge}");
                            }

                            result.AppendLine($"    Weight: {player.Weight} kg");
                            result.AppendLine($"    Height: {player.Height} cm");
                            result.AppendLine($"    Ranking: {player.Ranking}");
                            result.AppendLine($"    City: {player.City.Name}, {player.City.Country.Name}");

                            var matchesWon = dp.Matches
                                .Find(m => m.Winner.Id == playerId)
                                .ToList();

                            var allPoints = 0;

                            foreach (Match match in matchesWon)
                            {
                                allPoints += dp.PointDistributions
                                    .Find(p => p.Round.Stage == match.Round.Stage &&
                                    p.TournamentCategory.Category == match.Tournament.Category.Category)
                                    .Select(p => p.Points)
                                    .FirstOrDefault();
                            }

                            result.AppendLine($"    Points won: {allPoints}");
                            result.AppendLine($"    Matches played: {matchesCount}");
                        }
                        else
                        {
                            if (i < players.Count())
                            {
                                continue;
                            }
                            else
                            {
                                throw new ArgumentException($"Sorry, no player with id {playerId} exists!");
                            }
                        }

                        i++;
                    }
                    else
                    {
                        throw new ArgumentException($"Sorry, {playerId} is not a valid number!");
                    }
                }
                else
                {
                    result.AppendLine($"Id: {player.Id} | Name: {player.FirstName} {player.LastName} | Matches Played: {matchesCount}");
                }
            }
            result.AppendLine("");
            result.AppendLine("[menu] [show] [showt] [showm]");
            return result.ToString();
        }
    }
}
