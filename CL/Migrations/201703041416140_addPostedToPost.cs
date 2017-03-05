namespace CL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPostedToPost : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostModels", "Category_Id", "dbo.CatModels");
            DropForeignKey("dbo.PostModels", "City_Id", "dbo.LocationModels");
            DropIndex("dbo.PostModels", new[] { "Category_Id" });
            DropIndex("dbo.PostModels", new[] { "City_Id" });
            RenameColumn(table: "dbo.PostModels", name: "Category_Id", newName: "CatId");
            RenameColumn(table: "dbo.PostModels", name: "City_Id", newName: "CityId");
            RenameColumn(table: "dbo.PostModels", name: "ThisUser_Id", newName: "OwnerId");
            RenameIndex(table: "dbo.PostModels", name: "IX_ThisUser_Id", newName: "IX_OwnerId");
            AddColumn("dbo.PostModels", "Posted", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PostModels", "CatId", c => c.Int(nullable: false));
            AlterColumn("dbo.PostModels", "CityId", c => c.Int(nullable: false));
            CreateIndex("dbo.PostModels", "CityId");
            CreateIndex("dbo.PostModels", "CatId");
            AddForeignKey("dbo.PostModels", "CatId", "dbo.CatModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PostModels", "CityId", "dbo.LocationModels", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostModels", "CityId", "dbo.LocationModels");
            DropForeignKey("dbo.PostModels", "CatId", "dbo.CatModels");
            DropIndex("dbo.PostModels", new[] { "CatId" });
            DropIndex("dbo.PostModels", new[] { "CityId" });
            AlterColumn("dbo.PostModels", "CityId", c => c.Int());
            AlterColumn("dbo.PostModels", "CatId", c => c.Int());
            DropColumn("dbo.PostModels", "Posted");
            RenameIndex(table: "dbo.PostModels", name: "IX_OwnerId", newName: "IX_ThisUser_Id");
            RenameColumn(table: "dbo.PostModels", name: "OwnerId", newName: "ThisUser_Id");
            RenameColumn(table: "dbo.PostModels", name: "CityId", newName: "City_Id");
            RenameColumn(table: "dbo.PostModels", name: "CatId", newName: "Category_Id");
            CreateIndex("dbo.PostModels", "City_Id");
            CreateIndex("dbo.PostModels", "Category_Id");
            AddForeignKey("dbo.PostModels", "City_Id", "dbo.LocationModels", "Id");
            AddForeignKey("dbo.PostModels", "Category_Id", "dbo.CatModels", "Id");
        }
    }
}
