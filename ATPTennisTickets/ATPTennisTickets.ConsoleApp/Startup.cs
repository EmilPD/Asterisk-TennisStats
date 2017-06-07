using ATPTennisTickets.Data;
using ATPTennisTickets.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisTickets.ConsoleApp
{
    class Startup
    {
        static void Main(string[] args)
        {
            // use standard EF API Context
            /*
            using (var ctx = new PostgresDbContext())
            {
                var tournament = new Tournament() { Name = "Roland Garros" };
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
                }
                ctx.SaveChanges();

                var list = ctx.Tickets.Select(t => t.Tournament).ToList();
                Console.WriteLine(String.Join(", ", list));
            }
            */

            // USE Example from Here http://www.npgsql.org/doc/index.html
            var connString = "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=atpTicketsDb";

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Insert some data
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO Tournaments (Name) VALUES (@p)";
                    cmd.Parameters.AddWithValue("p", "Roland Garros");
                    cmd.ExecuteNonQuery();
                }

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand("SELECT Name FROM Tournaments", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        Console.WriteLine(reader.GetString(0));
            }
        }
    }
}
