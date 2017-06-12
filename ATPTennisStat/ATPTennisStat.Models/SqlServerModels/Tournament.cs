using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATPTennisStat.Models.SqlServerModels
{
    public class Tournament
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime EndDate { get; set; }

        [Column(TypeName = "money")]
        public decimal PrizeMoney { get; set; }

        [Required]
        public virtual Surface Type { get; set; }

        [Required]
        public virtual TournamentCategory Category { get; set; }

        [Required]
        public virtual City City { get; set; } // One City - many Tournaments
    }
}