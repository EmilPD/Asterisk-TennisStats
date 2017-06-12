using ATPTennisTickets.Data;
using ATPTennisTickets.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATPTennisTickets.Data.Migrations;

namespace ATPTennisTickets.ConsoleApp
{
    class Startup
    {
        static void Main(string[] args)
        {
            // use standard EF API Context
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PostgresDbContext, Configuration>());

            using (var ctx = new PostgresDbContext())
            {
                var tournament = new Tournament()
                {
                    Name = "Roland Garros"

                };
                ctx.Tournaments.Add(tournament);
                ctx.SaveChanges();

                var id = ctx.Tournaments.Where(t => t.Name == "Roland Garros").Select(n => n.Id).FirstOrDefault();
                Console.WriteLine("Tournament Id: " + id);

              
                for (int i = 0; i < 10; i++)
                {
                    var ticket = new Ticket()
                    {
                        Sector = Sector.Front,
                        Price = 5.40m,
                        Number = 200,
                        TournamentId = id
                    };
                    ctx.Tickets.Add(ticket);

                }
                ctx.SaveChanges();

                var list = ctx.Tickets.Select(t => t.Tournament).ToList();
                Console.WriteLine(String.Join(", ", list));
            }

        }
    }
}
