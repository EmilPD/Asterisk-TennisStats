using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATPTennisStat.Models.SqlServerModels
{
    public class PointDistribution
    {
        [Key, Column(Order = 0)]
        public int TournamentCategoryId { get; set; }

        [Key, Column(Order = 1)]
        public int RoundId { get; set; }
   
        public virtual TournamentCategory TournamentCategory { get; set; }

        public virtual Round Round { get; set; }

        public int Points { get; set; }
    }
}