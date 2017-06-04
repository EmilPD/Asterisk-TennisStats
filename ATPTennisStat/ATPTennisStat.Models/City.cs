﻿using System.Collections.Generic;

namespace ATPTennisStat.Models
{
    public class City
    {
        public City()
        {
            Players = new HashSet<Player>();
            //Tournaments = new HashSet<Tournament>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public int TournamentId { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        //public virtual ICollection<Tournament> Tournaments { get; set; }
    }
}