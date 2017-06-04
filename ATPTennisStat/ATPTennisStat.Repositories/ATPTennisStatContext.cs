using System.Data.Entity;

namespace ATPTennisStat.Repositories
{
    public class ATPTennisStatContext : DbContext
    {
        public ATPTennisStatContext()
            : base("ATPTennisStatContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
    }
}
