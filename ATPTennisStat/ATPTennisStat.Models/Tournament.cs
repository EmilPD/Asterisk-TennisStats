using System;

namespace ATPTennisStat.Models
{
    public class Tournament
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal PrizeMoney { get; set; }

        public virtual Surface Type { get; set; }

        public virtual TournamentCategory Category { get; set; }

        public virtual City City { get; set; } // One City - many Tournaments
    }
}
