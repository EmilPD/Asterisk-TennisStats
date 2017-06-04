using System.Data.Entity.ModelConfiguration;
using ATPTennisStat.Models;

namespace ATPTennisStat.SQLServerData
{
    internal class UmpireConfiguration : EntityTypeConfiguration<Umpire>
    {
        public UmpireConfiguration()
        {
            this.HasKey(u => u.Id);

            this.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(u => u.YearActiveFrom)
                .HasColumnType("smallint");
        }
    }
}