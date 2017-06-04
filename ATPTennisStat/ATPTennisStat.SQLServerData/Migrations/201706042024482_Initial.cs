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
                        Name = c.String(nullable: false, maxLength: 50),
                        Country_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id, cascadeDelete: true)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Height = c.Single(nullable: false),
                        Weight = c.Single(nullable: false),
                        BirthDate = c.DateTime(storeType: "smalldatetime"),
                        Ranking = c.Int(nullable: false),
                        City_Id = c.Int(),
                        Coach_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.City_Id)
                .ForeignKey("dbo.Coaches", t => t.Coach_Id, cascadeDelete: true)
                .Index(t => t.FirstName, unique: true)
                .Index(t => t.LastName, unique: true)
                .Index(t => t.City_Id)
                .Index(t => t.Coach_Id);
            
            CreateTable(
                "dbo.Coaches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(storeType: "smalldatetime"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.FirstName, unique: true)
                .Index(t => t.LastName, unique: true);
            
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        StartDate = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        EndDate = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        PrizeMoney = c.Decimal(nullable: false, storeType: "money"),
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
                        Category = c.String(nullable: false, maxLength: 40),
                        PlayersCount = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Surfaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 40),
                        Speed = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Umpires",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 40),
                        LastName = c.String(nullable: false, maxLength: 40),
                        YearActiveFrom = c.Short(nullable: false),
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
            DropIndex("dbo.Coaches", new[] { "LastName" });
            DropIndex("dbo.Coaches", new[] { "FirstName" });
            DropIndex("dbo.Players", new[] { "Coach_Id" });
            DropIndex("dbo.Players", new[] { "City_Id" });
            DropIndex("dbo.Players", new[] { "LastName" });
            DropIndex("dbo.Players", new[] { "FirstName" });
            DropIndex("dbo.Cities", new[] { "Country_Id" });
            DropIndex("dbo.Cities", new[] { "Name" });
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
