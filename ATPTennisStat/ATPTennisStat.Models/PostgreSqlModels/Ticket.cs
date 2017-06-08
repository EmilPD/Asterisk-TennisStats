using ATPTennisStat.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATPTennisStat.Models.PostgreSqlModels
{
    [Table("Tickets", Schema = "public")]
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        public Sector Sector { get; set; }

        //[Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int? Number { get; set; }

        public virtual int TennisEventId { get; set; }

        public virtual TennisEvent TennisEvent { get; set; }
    }
}
