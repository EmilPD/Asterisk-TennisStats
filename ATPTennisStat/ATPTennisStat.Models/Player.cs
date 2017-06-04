using System;
using System.Collections.Generic;

namespace ATPTennisStat.Models
{
    public class Player
    {
        public Player()
        {
            //Matches = new HashSet<Match>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public double Height { get; set; }

        public double Weight { get; set; }

        public DateTime BirthDate { get; set; }

        public int Ranking { get; set; }

        public virtual City City { get; set; }

        public virtual Coach Coach { get; set; }

        //public virtual ICollection<Match> Matches { get; set; }
    }
}
