namespace CL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userfix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserModels", "Location_Id", "dbo.LocationModels");
            DropIndex("dbo.UserModels", new[] { "Location_Id" });
            RenameColumn(table: "dbo.UserModels", name: "Location_Id", newName: "CityId");
            RenameColumn(table: "dbo.UserModels", name: "Username_Id", newName: "OwnerId");
            RenameIndex(table: "dbo.UserModels", name: "IX_Username_Id", newName: "IX_OwnerId");
            AlterColumn("dbo.UserModels", "CityId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserModels", "CityId");
            AddForeignKey("dbo.UserModels", "CityId", "dbo.LocationModels", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserModels", "CityId", "dbo.LocationModels");
            DropIndex("dbo.UserModels", new[] { "CityId" });
            AlterColumn("dbo.UserModels", "CityId", c => c.Int());
            RenameIndex(table: "dbo.UserModels", name: "IX_OwnerId", newName: "IX_Username_Id");
            RenameColumn(table: "dbo.UserModels", name: "OwnerId", newName: "Username_Id");
            RenameColumn(table: "dbo.UserModels", name: "CityId", newName: "Location_Id");
            CreateIndex("dbo.UserModels", "Location_Id");
            AddForeignKey("dbo.UserModels", "Location_Id", "dbo.LocationModels", "Id");
        }
    }
}
