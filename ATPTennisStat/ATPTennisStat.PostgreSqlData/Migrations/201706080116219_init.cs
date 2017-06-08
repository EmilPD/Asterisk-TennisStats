namespace ATPTennisStat.PostgreSqlData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.tennisevent",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "public.Tickets",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        sector = c.Int(nullable: false),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        number = c.Int(),
                        tenniseventid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("public.tennisevent", t => t.tenniseventid, cascadeDelete: true)
                .Index(t => t.tenniseventid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.Tickets", "tenniseventid", "public.tennisevent");
            DropIndex("public.Tickets", new[] { "tenniseventid" });
            DropTable("public.Tickets");
            DropTable("public.tennisevent");
        }
    }
}
