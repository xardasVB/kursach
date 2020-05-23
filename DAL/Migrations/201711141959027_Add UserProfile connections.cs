namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserProfileconnections : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "BasketId", "dbo.Baskets");
            DropForeignKey("dbo.Orders", "ShoppingHistoryId", "dbo.ShoppingHistories");
            DropIndex("dbo.Orders", new[] { "BasketId" });
            DropIndex("dbo.Orders", new[] { "ShoppingHistoryId" });
            DropPrimaryKey("dbo.Baskets");
            DropPrimaryKey("dbo.ShoppingHistories");
            AlterColumn("dbo.Baskets", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Orders", "BasketId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Orders", "ShoppingHistoryId", c => c.String(maxLength: 128));
            AlterColumn("dbo.ShoppingHistories", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Baskets", "Id");
            AddPrimaryKey("dbo.ShoppingHistories", "Id");
            CreateIndex("dbo.Baskets", "Id");
            CreateIndex("dbo.Orders", "BasketId");
            CreateIndex("dbo.Orders", "ShoppingHistoryId");
            CreateIndex("dbo.ShoppingHistories", "Id");
            AddForeignKey("dbo.ShoppingHistories", "Id", "dbo.UserProfiles", "Id");
            AddForeignKey("dbo.Baskets", "Id", "dbo.UserProfiles", "Id");
            AddForeignKey("dbo.Orders", "BasketId", "dbo.Baskets", "Id");
            AddForeignKey("dbo.Orders", "ShoppingHistoryId", "dbo.ShoppingHistories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ShoppingHistoryId", "dbo.ShoppingHistories");
            DropForeignKey("dbo.Orders", "BasketId", "dbo.Baskets");
            DropForeignKey("dbo.Baskets", "Id", "dbo.UserProfiles");
            DropForeignKey("dbo.ShoppingHistories", "Id", "dbo.UserProfiles");
            DropIndex("dbo.ShoppingHistories", new[] { "Id" });
            DropIndex("dbo.Orders", new[] { "ShoppingHistoryId" });
            DropIndex("dbo.Orders", new[] { "BasketId" });
            DropIndex("dbo.Baskets", new[] { "Id" });
            DropPrimaryKey("dbo.ShoppingHistories");
            DropPrimaryKey("dbo.Baskets");
            AlterColumn("dbo.ShoppingHistories", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Orders", "ShoppingHistoryId", c => c.Int());
            AlterColumn("dbo.Orders", "BasketId", c => c.Int());
            AlterColumn("dbo.Baskets", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ShoppingHistories", "Id");
            AddPrimaryKey("dbo.Baskets", "Id");
            CreateIndex("dbo.Orders", "ShoppingHistoryId");
            CreateIndex("dbo.Orders", "BasketId");
            AddForeignKey("dbo.Orders", "ShoppingHistoryId", "dbo.ShoppingHistories", "Id");
            AddForeignKey("dbo.Orders", "BasketId", "dbo.Baskets", "Id");
        }
    }
}
