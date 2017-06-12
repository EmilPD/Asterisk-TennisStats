using System.Data.Entity.ModelConfiguration;
using ATPTennisStat.Models.SqlServerModels;

namespace ATPTennisStat.SQLServerData.EntityConfigurations
{
    public class MatchConfiguration : EntityTypeConfiguration<Match>
    {
        public MatchConfiguration()
        {
            this.HasRequired(m => m.Winner)
                    .WithMany(p => p.WonMatches)
                    .WillCascadeOnDelete(false);

            this.HasRequired(m => m.Loser)
                   .WithMany(p => p.LostMatches)
                   .WillCascadeOnDelete(false);
        }
    }
}