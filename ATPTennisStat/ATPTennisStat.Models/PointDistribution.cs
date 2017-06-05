using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.Models
{
    public class PointDistribution
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TournamentCategoryId { get; set; }

        [Required]
        public int RoundId { get; set; }

        public virtual TournamentCategory TournamentCategory { get; set; }

        public virtual Round Round { get; set; }

        [Required]
        public int Points { get; set; }


    }
}
