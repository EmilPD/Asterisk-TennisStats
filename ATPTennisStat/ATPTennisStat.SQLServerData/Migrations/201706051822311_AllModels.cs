namespace ATPTennisStat.SQLServerData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Result = c.String(maxLength: 40),
                        DatePlayed = c.DateTime(storeType: "smalldatetime"),
                        Loser_Id = c.Int(nullable: false),
                        Round_Id = c.Int(),
                        Tournament_Id = c.Int(),
                        Umpire_Id = c.Int(),
                        Winner_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.Loser_Id)
                .ForeignKey("dbo.Rounds", t => t.Round_Id)
                .ForeignKey("dbo.Tournaments", t => t.Tournament_Id)
                .ForeignKey("dbo.Umpires", t => t.Umpire_Id)
                .ForeignKey("dbo.Players", t => t.Winner_Id)
                .Index(t => t.Loser_Id)
                .Index(t => t.Round_Id)
                .Index(t => t.Tournament_Id)
                .Index(t => t.Umpire_Id)
                .Index(t => t.Winner_Id);
            
            CreateTable(
                "dbo.Rounds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Stage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PointDistributions",
                c => new
                    {
                        TournamentCategoryId = c.Int(nullable: false),
                        RoundId = c.Int(nullable: false),
                        Points = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TournamentCategoryId, t.RoundId })
                .ForeignKey("dbo.Rounds", t => t.RoundId, cascadeDelete: true)
                .ForeignKey("dbo.TournamentCategories", t => t.TournamentCategoryId, cascadeDelete: true)
                .Index(t => t.TournamentCategoryId)
                .Index(t => t.RoundId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "Winner_Id", "dbo.Players");
            DropForeignKey("dbo.Matches", "Umpire_Id", "dbo.Umpires");
            DropForeignKey("dbo.Matches", "Tournament_Id", "dbo.Tournaments");
            DropForeignKey("dbo.PointDistributions", "TournamentCategoryId", "dbo.TournamentCategories");
            DropForeignKey("dbo.PointDistributions", "RoundId", "dbo.Rounds");
            DropForeignKey("dbo.Matches", "Round_Id", "dbo.Rounds");
            DropForeignKey("dbo.Matches", "Loser_Id", "dbo.Players");
            DropIndex("dbo.PointDistributions", new[] { "RoundId" });
            DropIndex("dbo.PointDistributions", new[] { "TournamentCategoryId" });
            DropIndex("dbo.Matches", new[] { "Winner_Id" });
            DropIndex("dbo.Matches", new[] { "Umpire_Id" });
            DropIndex("dbo.Matches", new[] { "Tournament_Id" });
            DropIndex("dbo.Matches", new[] { "Round_Id" });
            DropIndex("dbo.Matches", new[] { "Loser_Id" });
            DropTable("dbo.PointDistributions");
            DropTable("dbo.Rounds");
            DropTable("dbo.Matches");
        }
    }
}
