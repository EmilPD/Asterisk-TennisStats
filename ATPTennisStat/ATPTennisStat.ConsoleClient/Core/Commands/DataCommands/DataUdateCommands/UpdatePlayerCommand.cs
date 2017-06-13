using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Factories.Contracts;
using ATPTennisStat.SQLServerData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataUdateCommands
{
    class UpdatePlayerCommand : ICommand
    {
        private ISqlServerDataProvider dp;
        private IWriter writer;

        public UpdatePlayerCommand(ISqlServerDataProvider sqlDP, IWriter writer)
        {
            this.dp = sqlDP;
            this.writer = writer;
        }

        public string Execute()
        {
            return $@"Not enough parameters!
Use this template [id param new-data].
1 - first name
2 - last name
3 - height
4 - weight
5 - birthday (yyyy/mm/dd)
6 - rank
7 - city";
        }

        public string Execute(IList<string> parameters)
        {
            writer.Clear();

            if (parameters.Count != 3)
            {
                return this.Execute();
            }
            else
            {
                int id = -1;
                bool parsed = int.TryParse(parameters[0], out id);

                if (!parsed)
                {
                    throw new ArgumentException("Provided Id is not valid!");
                }

                var player = dp.Players.Get(id);
                if (player == null)
                {
                    throw new ArgumentNullException("No player with this Id in database!");
                }

                int param = -1;
                bool paramParsed = int.TryParse(parameters[1], out param);
                if (!paramParsed)
                {
                    throw new ArgumentException("Provided parameter is not valid!");
                }

                string data = parameters[2];

                switch (param)
                {
                    case 1: { player.FirstName = data; break; }
                    case 2: { player.LastName = data; break; }
                    case 3:
                        {
                            try
                            {
                                player.Height = float.Parse(data);
                            }
                            catch (Exception)
                            {
                                throw new ArgumentException("Player's height must be a number");
                            }
                            break;
                        }
                    case 4:
                        {
                            try
                            {
                                player.Weight = float.Parse(data);
                            }
                            catch (Exception)
                            {

                                throw new ArgumentException("Player's weight must be a number");
                            }
                            break;
                        }

                    case 5:
                        {
                            try
                            {
                                player.BirthDate = DateTime.Parse(data);
                            }
                            catch (Exception)
                            {

                                throw new ArgumentException("Please enter a date in a valid format, e.g. dd/mm/yyyy");
                            }

                            break;
                        }
                    case 6:
                        {
                            try
                            {
                                player.Ranking = int.Parse(data);

                            }
                            catch (Exception)
                            {

                                throw new ArgumentException("Please enter an integer number");
                            }

                            break;
                        }
                    case 7:
                        {
                            var foundCity = this.dp.Cities.GetAll()
                                            .FirstOrDefault(c => c.Name == data);
                            if (foundCity == null)
                            {
                                throw new ArgumentException("The entered city is not found in the database." + 
                                                        "\n\r"+
                                                        "Please add it via the \"addct\" command.");
                            }
                            player.City = foundCity;
                            break;
                        }
                    default:
                        throw new ArgumentOutOfRangeException("Parameter must be between 1 and 7!");
                }
                this.dp.UnitOfWork.Finished();
                return $"Player with {player.Id} updated successfully!";
            }
        }
    }
}
