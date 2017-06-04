using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using ATPTennisStat.Models;

namespace ATPTennisStat.SQLServerData.EntityConfigurations
{
    public class PlayerConfiguration : EntityTypeConfiguration<Player>
    {
        public PlayerConfiguration()
        {
            Property(p => p.FirstName)
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

            Property(p => p.LastName)
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

            Property(p => p.BirthDate)
                .HasColumnType("smalldatetime")
                .IsOptional();
        }
    }
}
