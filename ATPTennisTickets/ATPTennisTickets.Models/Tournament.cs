using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisTickets.Models
{
    //[Table("Tournaments", Schema="public")]
    public class Tournament
    {
        private ICollection<Ticket> tickets;

        public Tournament()
        {
            this.tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Ticket> Tickets
        {
            get { return this.tickets; }
            set { this.tickets = value; }
        }
    }
}
