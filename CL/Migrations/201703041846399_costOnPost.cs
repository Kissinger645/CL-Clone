namespace CL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class costOnPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostModels", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostModels", "Price");
        }
    }
}
