using System.Collections.Generic;

namespace ATPTennisStat.Models.SqlServerModels
{
    public class TournamentCategory
    {
        private ICollection<PointDistribution> pointDistributions;

        public TournamentCategory()
        {
            this.pointDistributions = new HashSet<PointDistribution>();
        }

        public int Id { get; set; }

        public string Category { get; set; }

        public byte PlayersCount { get; set; }

        public virtual ICollection<PointDistribution> PointDistributions
        {
            get { return this.pointDistributions; }
            set { this.pointDistributions = value; }
        }
    }
}