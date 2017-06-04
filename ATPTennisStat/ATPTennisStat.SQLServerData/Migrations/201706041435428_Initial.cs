namespace ATPTennisStat.SQLServerData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Height = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        Ranking = c.Int(nullable: false),
                        City_Id = c.Int(),
                        Coach_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.City_Id)
                .ForeignKey("dbo.Coaches", t => t.Coach_Id)
                .Index(t => t.City_Id)
                .Index(t => t.Coach_Id);
            
            CreateTable(
                "dbo.Coaches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        PrizeMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category_Id = c.Int(),
                        City_Id = c.Int(),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TournamentCategories", t => t.Category_Id)
                .ForeignKey("dbo.Cities", t => t.City_Id)
                .ForeignKey("dbo.Surfaces", t => t.Type_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.City_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.TournamentCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Surfaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Speed = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Umpires",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .Index(t => t.Country_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Umpires", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.Tournaments", "Type_Id", "dbo.Surfaces");
            DropForeignKey("dbo.Tournaments", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.Tournaments", "Category_Id", "dbo.TournamentCategories");
            DropForeignKey("dbo.Players", "Coach_Id", "dbo.Coaches");
            DropForeignKey("dbo.Players", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.Cities", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Umpires", new[] { "Country_Id" });
            DropIndex("dbo.Tournaments", new[] { "Type_Id" });
            DropIndex("dbo.Tournaments", new[] { "City_Id" });
            DropIndex("dbo.Tournaments", new[] { "Category_Id" });
            DropIndex("dbo.Players", new[] { "Coach_Id" });
            DropIndex("dbo.Players", new[] { "City_Id" });
            DropIndex("dbo.Cities", new[] { "Country_Id" });
            DropTable("dbo.Umpires");
            DropTable("dbo.Surfaces");
            DropTable("dbo.TournamentCategories");
            DropTable("dbo.Tournaments");
            DropTable("dbo.Coaches");
            DropTable("dbo.Players");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
        }
    }
}
