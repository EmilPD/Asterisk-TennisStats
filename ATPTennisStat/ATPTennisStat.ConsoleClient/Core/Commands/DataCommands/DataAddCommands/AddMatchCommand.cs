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
            if (sqlDP == null)
            {
                throw new ArgumentNullException("Data provider cannot be null!");
            }

            if (writer == null)
            {
                throw new ArgumentNullException("Writer cannot be null!");
            }

            if (factory == null)
            {
                throw new ArgumentNullException("Writer cannot be null!");
            }

            this.dp = sqlDP;
            this.writer = writer;
            this.factory = factory;
        }

        public string Execute()
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

        public string Execute(IList<string> parameters)
        {
            writer.Clear();

            if(parameters.Count < 8)
            {
                return this.Execute();
            }
            else
            {
                int tournamentId = -1;

                var datePlayed = parameters[0];

                var wfirstName = parameters[1];
                var wlastName = parameters[2];
                string winner = $"{wfirstName} {wlastName}";

                var lfirstName = parameters[3];
                var llastName = parameters[4];
                string loser = $"{lfirstName} {llastName}";

                var result = parameters[5];

                tournamentId = int.Parse(parameters[6]);
                string tournament = dp.Tournaments.Get(tournamentId).Name;

                if (tournament == null)
                {
                    throw new ArgumentNullException($"No tournament with id {tournamentId} found!");
                }

                var round = parameters[7];

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
        }
    }
}