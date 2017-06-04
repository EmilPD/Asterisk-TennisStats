namespace ATPTennisStat.SQLServerData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedConstraintsForCoach : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Coaches", "FirstName", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Coaches", "LastName", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Coaches", "BirthDate", c => c.DateTime(nullable: false, storeType: "smalldatetime"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Coaches", "BirthDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Coaches", "LastName", c => c.String());
            AlterColumn("dbo.Coaches", "FirstName", c => c.String());
        }
    }
}
