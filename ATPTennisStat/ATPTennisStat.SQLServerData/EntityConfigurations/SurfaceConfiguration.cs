using System.Data.Entity.ModelConfiguration;
using ATPTennisStat.Models;

namespace ATPTennisStat.SQLServerData
{
    internal class SurfaceConfiguration : EntityTypeConfiguration<Surface>
    {
        public SurfaceConfiguration()
        {
            this.HasKey(s => s.Id);

            this.Property(s => s.Type)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(s => s.Speed)
                .HasMaxLength(40);
        }
    }
}