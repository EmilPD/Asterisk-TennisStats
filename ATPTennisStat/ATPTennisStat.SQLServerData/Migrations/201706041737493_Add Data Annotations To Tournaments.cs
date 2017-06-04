namespace ATPTennisStat.SQLServerData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataAnnotationsToTournaments : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tournaments", "Name", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Tournaments", "StartDate", c => c.DateTime(nullable: false, storeType: "smalldatetime"));
            AlterColumn("dbo.Tournaments", "EndDate", c => c.DateTime(nullable: false, storeType: "smalldatetime"));
            AlterColumn("dbo.Tournaments", "PrizeMoney", c => c.Decimal(nullable: false, storeType: "money"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tournaments", "PrizeMoney", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Tournaments", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Tournaments", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Tournaments", "Name", c => c.String());
        }
    }
}
