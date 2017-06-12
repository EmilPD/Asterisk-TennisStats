using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Factories.Contracts;
using ATPTennisStat.SQLServerData;
using System;
using System.Collections.Generic;

namespace ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataShowCommands
{
    public class AddMatchCommand : ICommand
    {
        private ISqlServerDataProvider dp;
        private IWriter writer;
        private IModelsFactory factory;

        public AddMatchCommand(ISqlServerDataProvider sqlDP, IWriter writer, IModelsFactory factory)
        {
            this.dp = sqlDP;
            this.writer = writer;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            writer.Clear();

            string datePlayed = string.Empty;
            string wfirstName = string.Empty;
            string wlastName = string.Empty;
            string lfirstName = string.Empty;
            string llastName = string.Empty;
            string result = string.Empty;
            int tournamentId = -1;
            string round = string.Empty;

            if (parameters.Count > 7)
            {
                datePlayed = parameters[0];

                wfirstName = parameters[1];
                wlastName = parameters[2];
                string winner = $"{wfirstName} {wlastName}";

                lfirstName = parameters[3];
                llastName = parameters[4];
                string loser = $"{lfirstName} {llastName}";

                result = parameters[5];

                tournamentId = int.Parse(parameters[6]);
                string tournament = dp.Tournaments.Get(tournamentId).Name;

                round = parameters[7];

                var match = factory.CreateMatch(datePlayed, winner, loser, result, tournament, round);
                if (match != null)
                {
                    dp.Matches.Add(match);
                    dp.UnitOfWork.Finished();
                    return $"Match {winner} vs {loser} from {datePlayed} created successfully!";
                }
                else
                {
                    throw new ArgumentNullException("Match cannot be null!");
                }
            }
            else
            {
                return $@"Not enough parameters!
Use this template [addm 1 2 3 4 5 6] and try again!
1 - date played (yyyy/mm/dd)
2 - winner names (provide 2 names)
3 - loser names (provide 2 names)
4 - result
5 - tournament id
6 - round

[menu] [show] [add]";
            }
        }
    }
}