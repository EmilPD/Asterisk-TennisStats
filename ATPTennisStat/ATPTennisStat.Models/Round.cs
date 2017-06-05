using ATPTennisStat.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPTennisStat.Models
{
    public class Round
    {
        private ICollection<Match> matches;

        public Round()
        {
            this.matches = new HashSet<Match>();
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

        public virtual ICollection<PointDistribution> PointDistributions { get; set; }

    }
}
