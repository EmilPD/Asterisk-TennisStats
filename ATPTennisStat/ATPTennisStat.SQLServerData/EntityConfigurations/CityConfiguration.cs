using System.Data.Entity.ModelConfiguration;
using ATPTennisStat.Models;

namespace ATPTennisStat.SQLServerData.EntityConfigurations
{
    public class CityConfiguration : EntityTypeConfiguration<City>
    {
        public CityConfiguration()
        {
            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            HasRequired(c => c.Country)
                .WithMany(a => a.Cities);
        }
    }
}
