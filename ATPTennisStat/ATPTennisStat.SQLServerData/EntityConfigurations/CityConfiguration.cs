using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using ATPTennisStat.Models;

namespace ATPTennisStat.SQLServerData.EntityConfigurations
{
    public class CityConfiguration : EntityTypeConfiguration<City>
    {
        public CityConfiguration()
        {
            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_Name", 1)
                        {
                            IsUnique = true,
                            IsClustered = false
                        }));

            HasRequired(c => c.Country)
                .WithMany(a => a.Cities);
        }
    }
}
