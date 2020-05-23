namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addnewfields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "PriceCMV", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "Description", c => c.String(maxLength: 1024));
            AddColumn("dbo.UserProfiles", "Address", c => c.String(maxLength: 1024));
            AddColumn("dbo.UserProfiles", "Banned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfiles", "Banned");
            DropColumn("dbo.UserProfiles", "Address");
            DropColumn("dbo.Products", "Description");
            DropColumn("dbo.Products", "PriceCMV");
        }
    }
}
