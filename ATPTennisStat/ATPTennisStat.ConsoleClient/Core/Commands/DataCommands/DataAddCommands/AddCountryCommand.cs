using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Models;
using ATPTennisStat.SQLServerData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATPTennisStat.ConsoleClient.Core.Commands.DataCommands.DataShowCommands
{
    public class AddCountryCommand : ICommand
    {
        private ISqlServerDataProvider dp;
        private IWriter writer;

        public AddCountryCommand(ISqlServerDataProvider sqlDP, IWriter writer)
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

        public string Execute(IList<string> parameters)
        {
            writer.Clear();

            string countryName = string.Empty;

            if (parameters.Count == 1)
            {
                countryName = parameters[0];

                Country country = dp.Countries
                    .Find(c => c.Name == countryName)
                    .FirstOrDefault();

                if (country == null)
                {
                    dp.Countries.Add(new Country()
                    {
                        Name = countryName
                    });

                    dp.UnitOfWork.Finished();

                    return $@"Country {countryName} was successfully added!

[menu] [show] [add]";
                }
                else
                {
                    return $@"Country {countryName} already exists in database. 
Add new country with command [addco (name)]!

[menu] [show] [add]";
                }
            }
            else
            {
                return $@"Not enough parameters!
Use this template [addco (name)] and try again!

[menu] [show] [add]";
            }
        }
    }
}