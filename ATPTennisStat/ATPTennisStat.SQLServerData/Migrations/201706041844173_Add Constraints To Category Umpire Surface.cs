namespace ATPTennisStat.SQLServerData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConstraintsToCategoryUmpireSurface : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TournamentCategories", "PlayersCount", c => c.Byte(nullable: false));
            AddColumn("dbo.Umpires", "YearActiveFrom", c => c.Short(nullable: false));
            AlterColumn("dbo.TournamentCategories", "Category", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Surfaces", "Type", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Surfaces", "Speed", c => c.String(maxLength: 40));
            AlterColumn("dbo.Umpires", "FirstName", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Umpires", "LastName", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Umpires", "LastName", c => c.String());
            AlterColumn("dbo.Umpires", "FirstName", c => c.String());
            AlterColumn("dbo.Surfaces", "Speed", c => c.String());
            AlterColumn("dbo.Surfaces", "Type", c => c.String());
            AlterColumn("dbo.TournamentCategories", "Category", c => c.String());
            DropColumn("dbo.Umpires", "YearActiveFrom");
            DropColumn("dbo.TournamentCategories", "PlayersCount");
        }
    }
}
