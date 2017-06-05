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
        public int Id { get; set; }

        [Required]
        public RoundStage Stage { get; set; }
    }
}
