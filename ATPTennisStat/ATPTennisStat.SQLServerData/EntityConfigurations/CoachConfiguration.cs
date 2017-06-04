using System.Data.Entity.ModelConfiguration;
using ATPTennisStat.Models;

namespace ATPTennisStat.SQLServerData
{
    internal class CoachConfiguration : EntityTypeConfiguration<Coach>
    {
        public CoachConfiguration()
        {
            this.HasKey(c => c.Id);

            this.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(c => c.BirthDate)
                .HasColumnType("smalldatetime");
        }
    }
}