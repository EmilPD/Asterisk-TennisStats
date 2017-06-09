using System.Data.Entity.ModelConfiguration;
using ATPTennisStat.Models.SqliteModels;

namespace ATPTennisStat.SQLiteData.EntityConfigurations
{
    public class LogConfigurations : EntityTypeConfiguration<Log>
    {
        public LogConfigurations()
        {
            Property(c => c.Message)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}