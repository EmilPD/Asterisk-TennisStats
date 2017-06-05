using System.Collections.Generic;

namespace ATPTennisStat.Models
{
    public class City
    {
        private ICollection<Player> players;

        private ICollection<Tournament> tournaments;

        public City()
        {
            this.players = new HashSet<Player>();
            this.tournaments = new HashSet<Tournament>();
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Player> Players
        {
            get { return this.players; }
            set { this.players = value; }
        }

        public virtual ICollection<Tournament> Tournaments
        {
            get { return this.tournaments; }
            set { this.tournaments = value; }
        }
    }
}
