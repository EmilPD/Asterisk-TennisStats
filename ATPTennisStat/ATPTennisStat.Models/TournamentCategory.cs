using System.Collections.Generic;

namespace ATPTennisStat.Models
{
    public class TournamentCategory
    {
        public int Id { get; set; }

        public string Category { get; set; }

        public byte PlayersCount { get; set; }

        public virtual ICollection<PointDistribution> PointDistributions { get; set; }

    }
}