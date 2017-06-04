using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using ATPTennisStat.Models;

namespace ATPTennisStat.SQLServerData.EntityConfigurations
{
    public class CoachConfiguration : EntityTypeConfiguration<Coach>
    {
        public CoachConfiguration()
        {
            Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_FirstName", 1)
                        {
                            IsUnique = true,
                            IsClustered = false
                        }));

            Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_LastName", 1)
                        {
                            IsUnique = true,
                            IsClustered = false
                        }));

            Property(c => c.BirthDate)
                .HasColumnType("smalldatetime")
                .IsOptional();

            HasMany(c => c.Players)
                .WithRequired(a => a.Coach);
        }
    }
}
