namespace ATPTennisStat.Models
{
    public class Umpire
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public short YearActiveFrom { get; set; } // not required

        public virtual Country Country { get; set; } // not required
    }
}
