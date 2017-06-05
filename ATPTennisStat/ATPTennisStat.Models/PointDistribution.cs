using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.Models
{
    public class PointDistribution
    {
        [Key, Column(Order = 0)]
        public int TournamentCategoryId { get; set; }

        [Key, Column(Order = 1)]
        public int RoundId { get; set; }

        public virtual TournamentCategory TournamentCategory { get; set; }

        public virtual Round Round { get; set; }

        [Required]
        public int Points { get; set; }


    }
}
