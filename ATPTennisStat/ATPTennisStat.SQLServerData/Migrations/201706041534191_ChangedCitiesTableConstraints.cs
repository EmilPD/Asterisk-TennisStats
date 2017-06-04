namespace ATPTennisStat.SQLServerData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedCitiesTableConstraints : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cities", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Cities", new[] { "Country_Id" });
            AlterColumn("dbo.Cities", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Cities", "Country_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Cities", "Country_Id");
            AddForeignKey("dbo.Cities", "Country_Id", "dbo.Countries", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cities", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Cities", new[] { "Country_Id" });
            AlterColumn("dbo.Cities", "Country_Id", c => c.Int());
            AlterColumn("dbo.Cities", "Name", c => c.String());
            CreateIndex("dbo.Cities", "Country_Id");
            AddForeignKey("dbo.Cities", "Country_Id", "dbo.Countries", "Id");
        }
    }
}
