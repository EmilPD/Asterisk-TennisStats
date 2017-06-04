using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ATPTennisStat.Models
{
    public class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
