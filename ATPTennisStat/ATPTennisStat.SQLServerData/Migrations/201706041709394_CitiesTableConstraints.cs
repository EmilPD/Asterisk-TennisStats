namespace ATPTennisStat.SQLServerData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CitiesTableConstraints : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cities", "Name", n => n.String(maxLength: 40));
            DropForeignKey("dbo.Cities", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Cities", new[] { "Country_Id" });
            AlterColumn("dbo.Cities", "Country_Id", c => c.Int());
            CreateIndex("dbo.Cities", "Name", unique: true);
            CreateIndex("dbo.Cities", "Country_Id");
            AddForeignKey("dbo.Cities", "Country_Id", "dbo.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cities", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Cities", new[] { "Country_Id" });
            DropIndex("dbo.Cities", new[] { "Name" });
            AlterColumn("dbo.Cities", "Country_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Cities", "Country_Id");
            AddForeignKey("dbo.Cities", "Country_Id", "dbo.Countries", "Id", cascadeDelete: true);
        }
    }
}
