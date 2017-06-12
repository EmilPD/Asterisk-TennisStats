using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATPTennisStat.Models.SqlServerModels
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        public virtual Player Winner { get; set; }

        public virtual Player Loser { get; set; }

        [MaxLength(40)]
        [Required]
        public string Result { get; set; }
        
        [Required]
        public virtual Tournament Tournament { get; set; }

        [Column(TypeName = "smalldatetime")]
        [Required]
        public DateTime DatePlayed { get; set; }

        [Required]
        public virtual Round Round { get; set; }
    }
}