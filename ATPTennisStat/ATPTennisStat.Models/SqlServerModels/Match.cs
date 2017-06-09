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
        
        public virtual Player Winner { get; set; }
        
        public virtual Player Loser { get; set; }

        [MaxLength(40)]
        public string Result { get; set; }
        
        public virtual Tournament Tournament { get; set; }

        [Column(TypeName = "smalldatetime")]
        [Required]
        public DateTime DatePlayed { get; set; }
        
        public virtual Round Round { get; set; }

    }
}
