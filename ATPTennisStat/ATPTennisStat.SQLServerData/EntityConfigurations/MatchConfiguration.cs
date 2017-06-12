using ATPTennisStat.Models.SqlServerModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
