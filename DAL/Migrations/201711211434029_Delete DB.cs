namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "BasketId", "dbo.Baskets");
            DropForeignKey("dbo.Manufacturers", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Products", "ManufacturerId", "dbo.Manufacturers");
            DropForeignKey("dbo.Reviews", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Subcategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Products", "SubcategoryId", "dbo.Subcategories");
            DropForeignKey("dbo.Orders", "ProductId", "dbo.Products");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserProfiles", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "UserProfile_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.ShoppingHistories", "Id", "dbo.UserProfiles");
            DropForeignKey("dbo.Orders", "ShoppingHistoryId", "dbo.ShoppingHistories");
            DropForeignKey("dbo.Baskets", "Id", "dbo.UserProfiles");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.Baskets", new[] { "Id" });
            DropIndex("dbo.Orders", new[] { "ProductId" });
            DropIndex("dbo.Orders", new[] { "BasketId" });
            DropIndex("dbo.Orders", new[] { "ShoppingHistoryId" });
            DropIndex("dbo.Products", new[] { "SubcategoryId" });
            DropIndex("dbo.Products", new[] { "ManufacturerId" });
            DropIndex("dbo.Manufacturers", new[] { "CountryId" });
            DropIndex("dbo.Reviews", new[] { "ProductId" });
            DropIndex("dbo.Reviews", new[] { "UserProfile_Id" });
            DropIndex("dbo.Subcategories", new[] { "CategoryId" });
            DropIndex("dbo.ShoppingHistories", new[] { "Id" });
            DropIndex("dbo.UserProfiles", new[] { "Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.Baskets");
            DropTable("dbo.Orders");
            DropTable("dbo.Products");
            DropTable("dbo.Manufacturers");
            DropTable("dbo.Countries");
            DropTable("dbo.Reviews");
            DropTable("dbo.Subcategories");
            DropTable("dbo.Categories");
            DropTable("dbo.ShoppingHistories");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId });
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId });
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(maxLength: 256),
                        LastName = c.String(maxLength: 256),
                        Image = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShoppingHistories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subcategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Content = c.String(nullable: false, maxLength: 1024),
                        ProductId = c.Int(nullable: false),
                        UserProfile_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockCount = c.Int(nullable: false),
                        Code = c.String(),
                        Image = c.String(maxLength: 256),
                        SubcategoryId = c.Int(nullable: false),
                        ManufacturerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        Date = c.DateTime(),
                        ProductId = c.Int(nullable: false),
                        BasketId = c.String(maxLength: 128),
                        ShoppingHistoryId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.AspNetRoles", "Name", unique: true, name: "RoleNameIndex");
            CreateIndex("dbo.AspNetUserRoles", "RoleId");
            CreateIndex("dbo.AspNetUserRoles", "UserId");
            CreateIndex("dbo.AspNetUserLogins", "UserId");
            CreateIndex("dbo.AspNetUserClaims", "UserId");
            CreateIndex("dbo.AspNetUsers", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.UserProfiles", "Id");
            CreateIndex("dbo.ShoppingHistories", "Id");
            CreateIndex("dbo.Subcategories", "CategoryId");
            CreateIndex("dbo.Reviews", "UserProfile_Id");
            CreateIndex("dbo.Reviews", "ProductId");
            CreateIndex("dbo.Manufacturers", "CountryId");
            CreateIndex("dbo.Products", "ManufacturerId");
            CreateIndex("dbo.Products", "SubcategoryId");
            CreateIndex("dbo.Orders", "ShoppingHistoryId");
            CreateIndex("dbo.Orders", "BasketId");
            CreateIndex("dbo.Orders", "ProductId");
            CreateIndex("dbo.Baskets", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Baskets", "Id", "dbo.UserProfiles", "Id");
            AddForeignKey("dbo.Orders", "ShoppingHistoryId", "dbo.ShoppingHistories", "Id");
            AddForeignKey("dbo.ShoppingHistories", "Id", "dbo.UserProfiles", "Id");
            AddForeignKey("dbo.Reviews", "UserProfile_Id", "dbo.UserProfiles", "Id");
            AddForeignKey("dbo.UserProfiles", "Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "SubcategoryId", "dbo.Subcategories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Subcategories", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Reviews", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "ManufacturerId", "dbo.Manufacturers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Manufacturers", "CountryId", "dbo.Countries", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "BasketId", "dbo.Baskets", "Id");
        }
    }
}
