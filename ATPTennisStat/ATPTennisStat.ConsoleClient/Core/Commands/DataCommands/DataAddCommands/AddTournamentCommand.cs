using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Factories.Contracts;
using ATPTennisStat.Models;
using ATPTennisStat.SQLServerData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataShowCommands
{
    class AddTournamentCommand : ICommand
    {
        private ISqlServerDataProvider dp;
        private IWriter writer;
        private IModelsFactory factory;

        public AddTournamentCommand(ISqlServerDataProvider sqlDP, IWriter writer, IModelsFactory factory)
        {
            this.dp = sqlDP;
            this.writer = writer;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            writer.Clear();

            string name = string.Empty;
            string startDate = string.Empty;
            string endDate = string.Empty;
            string prizeMoney = string.Empty;
            string categoryName = string.Empty;
            string playersCount = string.Empty;
            string cityName = string.Empty;
            string countryName = string.Empty;
            string surfaceType = string.Empty;
            string surfaceSpeed = string.Empty;

            if (parameters.Count > 3)
            {
                name = parameters[0];
                surfaceType = parameters[1];
                categoryName = parameters[2];
                cityName = parameters[3];

                if (parameters.Count > 4)
                {
                    startDate = parameters[4];
                }

                if (parameters.Count > 5)
                {
                    endDate = parameters[5];
                }

                if (parameters.Count > 6)
                {
                    prizeMoney = parameters[6];
                }

                if (parameters.Count > 7)
                {
                    playersCount = parameters[7];
                }

                if (parameters.Count > 8)
                {
                    countryName = parameters[8];
                }

                surfaceSpeed = dp.Surfaces
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
            else
            {
                return $@"Not enough parameters!
Use this template [addt 1 2 3 4 5 6 7 8 9] and try again!
1 - name
2 - surface (clay, hard, grass)
3 - category
4 - city
5 - start (yyyy/mm/dd)
6 - end (yyyy/mm/dd)
7 - prize money (optional)
8 - players (optional)
9 - country (optional)

[menu] [show] [add]";
            }
        }
    }
}