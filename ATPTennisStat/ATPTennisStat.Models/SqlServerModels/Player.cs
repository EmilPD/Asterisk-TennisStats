using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ATPTennisStat.Models
{
    public class Player
    {
        private ICollection<Match> wonMatches;
        private ICollection<Match> lostMatches;
        private int totalPoints;


        public Player()
        {
            this.wonMatches = new HashSet<Match>();
            this.lostMatches = new HashSet<Match>();

        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public float? Height { get; set; }

        public float? Weight { get; set; }

        public DateTime? BirthDate { get; set; }

        [NotMapped]
        public int TotalPoints
        {
            get
            {
                var list = this.wonMatches
                                    .Select(wM => new
                                    {
                                        Points = wM.Tournament.Category.PointDistributions
                                            .Where(pd => pd.Round.Id == wM.Round.Id)
                                            .FirstOrDefault()
                                            .Points

                                    })
                                    .ToList();

                this.totalPoints = list.Sum(t => t.Points);

                return totalPoints;

            }
        }
        public int? Ranking { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<Match> WonMatches
        {
            get { return this.wonMatches; }
            set { this.wonMatches = value; }
        }

        public virtual ICollection<Match> LostMatches
        {
            get { return this.lostMatches; }
            set { this.lostMatches = value; }
        }
    }
}
