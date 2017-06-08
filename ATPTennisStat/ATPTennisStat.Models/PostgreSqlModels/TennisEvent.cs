using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATPTennisStat.Models.PostgreSqlModels
{
    [Table("tennisevent", Schema = "public")]
    public class TennisEvent
    {
        private ICollection<Ticket> tickets;

        public TennisEvent()
        {
            this.tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        //[Column(TypeName = "character varying")]
        public string Name { get; set; }

        public virtual ICollection<Ticket> Tickets
        {
            get { return this.tickets; }
            set { this.tickets = value; }
        }
    }
}
