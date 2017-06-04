using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ATPTennisStat.Models
{
    public class Coach
    {
        public Coach()
        {
            Players = new HashSet<Player>();
        }
        
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
