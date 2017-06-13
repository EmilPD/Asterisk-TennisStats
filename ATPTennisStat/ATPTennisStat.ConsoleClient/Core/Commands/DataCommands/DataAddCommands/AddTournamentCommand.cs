using System;
using System.Collections.Generic;
using System.Linq;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Factories.Contracts;
using ATPTennisStat.Models.SqlServerModels;
using ATPTennisStat.SQLServerData;

namespace ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataShowCommands
{
    public class AddTournamentCommand : ICommand
    {
        private ISqlServerDataProvider dp;
        private IWriter writer;
        private IModelsFactory factory;

        public AddTournamentCommand(ISqlServerDataProvider sqlDP, IWriter writer, IModelsFactory factory)
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
Use this template [addt 1 2 3 4 5 6 7 8 9] and try again!
1 - name
2 - surface (clay, hard, grass)
3 - category
4 - city
5 - start (yyyy/mm/dd)
6 - end (yyyy/mm/dd)
7 - prize money
8 - players
9 - country

[menu] [show] [add]";
        }

        public string Execute(IList<string> parameters)
        {
            writer.Clear();

            if (parameters.Count < 9)
            {
                return this.Execute();
            }
            else
            {

                var name = parameters[0];
                var surfaceType = parameters[1];
                var categoryName = parameters[2];
                var cityName = parameters[3];

                var startDate = parameters[4];
                var endDate = parameters[5];

                var prizeMoney = parameters[6];

                var playersCount = parameters[7];

                var countryName = parameters[8];

                var surfaceSpeed = dp.Surfaces
                    .Find(s => s.Type == surfaceType)
                    .Select(t => t.Speed)
                    .FirstOrDefault();

                Tournament tournament = factory.CreateTournament(name,
                                                      startDate,
                                                      endDate,
                                                      prizeMoney,
                                                      categoryName,
                                                      playersCount,
                                                      cityName,
                                                      countryName,
                                                      surfaceType,
                                                      surfaceSpeed);
                if (tournament != null)
                {
                    dp.Tournaments.Add(tournament);
                    dp.UnitOfWork.Finished();
                    return $"Tournament {name} created successfully!";
                }
                else
                {
                    throw new ArgumentNullException("Tournament cannot be null!");
                }
            }
        }
    }
}