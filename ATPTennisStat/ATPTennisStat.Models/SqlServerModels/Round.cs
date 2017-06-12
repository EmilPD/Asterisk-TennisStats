using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ATPTennisStat.Models.Enums;

namespace ATPTennisStat.Models.SqlServerModels
{
    public class Round
    {
        private ICollection<Match> matches;
        private ICollection<PointDistribution> pointDistributions;


        public Round()
        {
            this.matches = new HashSet<Match>();
            this.pointDistributions = new HashSet<PointDistribution>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public RoundStage Stage { get; set; }

        public virtual ICollection<Match> Matches
        {
            get { return this.matches; }
            set { this.matches = value; }
        }

        public virtual ICollection<PointDistribution> PointDistributions
        {
            get { return this.pointDistributions; }
            set { this.pointDistributions = value; }
        }

    }
}