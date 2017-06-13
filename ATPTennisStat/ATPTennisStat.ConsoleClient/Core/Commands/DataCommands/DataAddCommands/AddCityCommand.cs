using System;
using System.Collections.Generic;
using System.Linq;
using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Models.SqlServerModels;
using ATPTennisStat.SQLServerData;

namespace ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataShowCommands
{
    public class AddCityCommand : ICommand
    {
        private ISqlServerDataProvider dp;
        private IWriter writer;

        public AddCityCommand(ISqlServerDataProvider sqlDP, IWriter writer)
        {
            if (sqlDP == null)
            {
                throw new ArgumentNullException("Data provider cannot be null!");
            }

            if (writer == null)
            {
                throw new ArgumentNullException("Writer cannot be null!");
            }

            this.dp = sqlDP;
            this.writer = writer;
        }

        public string Execute()
        {
            return $@"Not enough parameters!
Use this template [addct (name) (country)] and try again!

[menu] [show] [add]";
        }

        public string Execute(IList<string> parameters)
        {
            writer.Clear();

            if(parameters.Count != 2)
            {
                return this.Execute();
            }
            else
            {
                var name = parameters[0];
                var countryName = parameters[1];

                var country = dp.Countries
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
        }
    }
}