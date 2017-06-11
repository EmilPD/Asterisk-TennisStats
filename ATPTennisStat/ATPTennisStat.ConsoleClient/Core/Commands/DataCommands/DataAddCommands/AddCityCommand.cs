using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Models;
using ATPTennisStat.SQLServerData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataShowCommands
{
    class AddCityCommand : ICommand
    {
        private ISqlServerDataProvider dp;
        private IWriter writer;

        public AddCityCommand(ISqlServerDataProvider sqlDP, IWriter writer)
        {
            this.dp = sqlDP;
            this.writer = writer;
        }

        public string Execute(IList<string> parameters)
        {
            writer.Clear();

            string name = string.Empty;
            string countryName = string.Empty;

            if (parameters.Count == 2)
            {
                name = parameters[0];
                countryName = parameters[1];

                Country country = dp.Countries
                    .Find(c => c.Name == countryName)
                    .FirstOrDefault();

                if (country != null)
                {
                    var search = dp.Cities
                        .Find(c => c.Name == name && c.Country.Name == countryName)
                        .FirstOrDefault();

                    if (search != null)
                    {
                        return $@"City {name}, {countryName} already exists in database. 
Add new country with command [addct (name) (country)]!

[menu] [show] [add]";
                    }

                    dp.Cities.Add(new City()
                    {
                        Name = name,
                        Country = country
                    });

                    dp.UnitOfWork.Finished();

                    return $@"City {name} was successfully added!

[menu] [show] [add]";
                }
                else
                {
                    return $@"No country {countryName} exists in database. 
Add new country with command [addct (name) (country)]!

[menu] [show] [add]";
                }
            }
            else
            {
                return $@"Not enough parameters!
Use this template [addct (name) (country)] and try again!

[menu] [show] [add]";
            }
        }
    }
}