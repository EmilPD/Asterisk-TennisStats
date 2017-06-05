using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATPTennisStat.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        public int WinnerId { get; set; }
        public virtual Player Winner { get; set; }

        public int LoserId { get; set; }
        public virtual Player Loser { get; set; }

        [MaxLength(40)]
        public string Result { get; set; }

        [Required]
        public int? TournamentId { get; set; }
        public virtual Tournament Tournament { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DatePlayed { get; set; }

        public int? UmpireId { get; set; }
        public virtual Umpire Umpire { get; set; }

        [Required]
        public int? RoundId { get; set; }
        public virtual Round Round { get; set; }

    }
}
