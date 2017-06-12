namespace ATPTennisTickets.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.Tickets",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        sector = c.Int(nullable: false),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        number = c.Int(nullable: false),
                        tournamentid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("public.Tournaments", t => t.tournamentid, cascadeDelete: true)
                .Index(t => t.tournamentid);
            
            CreateTable(
                "public.Tournaments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.Tickets", "tournamentid", "public.Tournaments");
            DropIndex("public.Tickets", new[] { "tournamentid" });
            DropTable("public.Tournaments");
            DropTable("public.Tickets");
        }
    }
}
