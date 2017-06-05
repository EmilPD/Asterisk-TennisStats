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
    }
}
