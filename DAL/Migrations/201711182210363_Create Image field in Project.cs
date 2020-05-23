namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateImagefieldinProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Image", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Image");
        }
    }
}
