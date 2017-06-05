using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ATPTennisStat.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }



        public int WinnerId { get; set; }
        public virtual Player Winner { get; set; }

        public int LoserId { get; set; }
        public virtual Player Loser { get; set; }

        //[MaxLength(40)]
        //public string Result { get; set; }

        //public int TournamentId { get; set; }
        //public virtual Tournament Tournament { get; set; }

        ////smalldatetime
        //public DateTime DatePlayed { get; set; }

        //public int UmpiredId { get; set; }
    }
}
