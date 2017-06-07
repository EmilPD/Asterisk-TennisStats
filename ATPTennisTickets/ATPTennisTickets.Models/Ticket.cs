using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisTickets.Models
{
    //[Table("Tickets", Schema = "public")]
    public class Ticket
    {
        public int Id { get; set; }

        public Sector Sector { get; set; }

        public decimal Price { get; set; }

        public int Number { get; set; }

        public virtual int TournamentId { get; set; }

        public virtual Tournament Tournament { get; set; }
    }
}
