using ATPTennisStat.ConsoleClient.Core.Contracts;
using ATPTennisStat.Factories.Contracts;
using ATPTennisStat.PostgreSqlData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ATPTennisStat.ConsoleClient.Core.Commands.TicketCommands
{
    class ImportTicketsListCommand : ICommand
    {
        protected readonly IPostgresDataProvider dp;
        private IWriter writer;
        private ITicketModelsFactory factory; 

        public ImportTicketsListCommand(IPostgresDataProvider dp, IWriter writer, ITicketModelsFactory factory)
        {
            if (dp == null)
            {
                throw new ArgumentNullException("Data provider cannot be null!");
            }

            if (writer == null)
            {
                throw new ArgumentNullException("Writer cannot be null!");
            }

            if (factory == null)
            {
                throw new ArgumentNullException("Factory cannot be null!");
            }

            this.dp = dp;
            this.writer = writer;
            this.factory = factory;
        }

        public string Execute()
        {
            return $@"Not enough parameters or file path is not correct!
Use this template [importm FILE_PATH] and try again!";
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count == 0)
            {
                return this.Execute();
            }
            else
            {
                var tickets = this.ImportTickets(parameters[0]);
                writer.WriteLine("Total records in dataset: " + tickets.Count);

                var dEvents = 0;
                var dTickets = 0;
                var tRecords = 0;
                var eRecords = 0;

                foreach (var t in tickets)
                {
                    
                    if (t.Name == null)
                    {
                        throw new ArgumentNullException("Event name must be provided!");
                    }

                    if (t.Sector == null)
                    {
                        throw new ArgumentNullException("Sector number must be provided!");
                    }

                    if (t.Price == null)
                    {
                        throw new ArgumentNullException("Ticket price must be provided!");
                    }

                    if (t.Number == null)
                    {
                        throw new ArgumentNullException("NUmber of tickets must be provided!");
                    }

                    var foundEvent = dp.TennisEvents.Find(e => e.Name == t.Name).FirstOrDefault();

                    if (foundEvent == null)
                    {
                        foundEvent = factory.CreateTennisEvent(t.Name);

                        if (foundEvent == null)
                        {
                            throw new ArgumentNullException("Cannot create event!");
                        }
                        else
                        {
                            dp.TennisEvents.Add(foundEvent);
                            dp.UnitOfWork.Finished();
                            eRecords++;
                        }
                    }
                    else
                    {
                        dEvents++;
                    }

                    var foundTicket = dp.Tickets.Find(f => f.TennisEvent.Name == t.Name && f.Sector.ToString() == t.Sector).FirstOrDefault();

                    if (foundTicket == null)
                    {
                        string eventId = dp.TennisEvents
                            .Find(e => e.Name == t.Name)
                            .Select(s => s.Id)
                            .FirstOrDefault()
                            .ToString();

                        foundTicket = factory.CreateTicket(t.Sector, t.Price, t.Number, eventId);

                        if (foundTicket == null)
                        {
                            throw new ArgumentNullException("Cannot create ticket!");
                        }
                        else
                        {
                            dp.Tickets.Add(foundTicket);
                            dp.UnitOfWork.Finished();
                            tRecords++;
                        }
                    }
                    else
                    {
                        dTickets++;
                    }
                }

                return $@"Successfully added data to database!
Added Tennis Events: {eRecords}
Added Ticket Records: {tRecords}
Duplicate Tennis Events: {dEvents}
Duplicate Ticket Records: {tRecords}

[menu]";
            }
        }

        public IList<TicketXmlImportModel> ImportTickets(string path)
        {
            var doc = XDocument.Load(path);
            return doc.Root
                .Elements("record")
                .Select(node =>
                {
                    var name = node.Element("Name").Value;
                    var sector = node.Element("Sector").Value;
                    var price = node.Element("Price").Value;
                    var number = node.Element("Number").Value;

                    return new TicketXmlImportModel()
                    {
                        Name = name,
                        Sector = sector,
                        Price = price,
                        Number = number
                    };
                })
                .ToList();
        }
    }

    public class TicketXmlImportModel
    {
        public string Name { get; set; }

        public string Sector { get; set; }

        public string Price { get; set; }

        public string Number { get; set; }
    }
}
