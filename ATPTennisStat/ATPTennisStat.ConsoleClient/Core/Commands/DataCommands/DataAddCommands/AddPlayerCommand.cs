using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Factories.Contracts;
using ATPTennisStat.SQLServerData;
using System;
using System.Collections.Generic;

namespace ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataShowCommands
{
    class AddPlayerCommand : ICommand
    {
        private SqlServerDataProvider dp;
        private IWriter writer;
        private IModelsFactory factory;

        public AddPlayerCommand(SqlServerDataProvider sqlDP, IWriter writer, IModelsFactory factory)
        {
            this.dp = sqlDP;
            this.writer = writer;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            writer.Clear();


            string firstName = string.Empty;
            string lastName = string.Empty;
            string weight = string.Empty;
            string height = string.Empty;
            string birthDate = string.Empty;
            string rank = string.Empty;
            string city = string.Empty;
            string country = string.Empty;

            if (parameters.Count > 1)
            {
                firstName = parameters[0];
                lastName = parameters[1];

                if (parameters.Count > 2)
                {
                    weight = parameters[2];
                }

                if (parameters.Count > 3)
                {
                    height = parameters[3];
                }

                if (parameters.Count > 4)
                {
                    birthDate = parameters[4];
                }

                if (parameters.Count > 5)
                {
                    rank = parameters[5];
                }

                if (parameters.Count > 6)
                {
                    city = parameters[6];
                }

                if (parameters.Count > 7)
                {
                    country = parameters[7];
                }

                var player = factory.CreatePlayer(firstName, lastName, rank, birthDate, height, weight, city, country);
                if (player != null)
                {
                    dp.Players.Add(player);
                    dp.UnitOfWork.Finished();
                    return $"PLayer {firstName} {lastName} created successfully!";
                }
                else
                {
                    throw new ArgumentNullException("Player cannot be null!");
                }
            }
            else
            {
                return $@"Not enough parameters!
Use this template [addct (F) (L) (H) (W) (B) (R) (C)] and try again!
F - first name
L - last name
H - height (optional)
W - weight (optional)
B - birthday (yyyy/mm/dd optional)
R - rank (optional)
C - city (optional)

[menu] [show] [add]";
            }
        }
    }
}