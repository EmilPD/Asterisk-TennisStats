using System.Data.Entity.ModelConfiguration;
using ATPTennisStat.Models;

namespace ATPTennisStat.SQLServerData.EntityConfigurations
{
    internal class TournamentCategoryConfiguration : EntityTypeConfiguration<TournamentCategory>
    {
        public TournamentCategoryConfiguration()
        {
            this.HasKey(t => t.Id);

            this.Property(t => t.Category)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.PlayersCount)
                .IsRequired()
                .HasColumnType("tinyint");
        }
    }
}