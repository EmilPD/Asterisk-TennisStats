namespace ATPTennisStat.Models
{
    class Umpire
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ushort YearActiveFrom { get; set; }

        public virtual Country Country { get; set; }
    }
}
