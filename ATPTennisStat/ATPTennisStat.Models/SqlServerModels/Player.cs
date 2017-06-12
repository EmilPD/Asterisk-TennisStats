using System;
using System.Collections.Generic;

namespace ATPTennisStat.Models.SqlServerModels
{
    public class Player
    {
        private ICollection<Match> wonMatches;
        private ICollection<Match> lostMatches;
        //private int totalPoints;


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