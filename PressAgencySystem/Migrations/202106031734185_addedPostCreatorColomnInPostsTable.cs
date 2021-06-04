namespace PressAgencySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPostCreatorColomnInPostsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "CreatorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "CreatorId");
            AddForeignKey("dbo.Posts", "CreatorId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "CreatorId", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "CreatorId" });
            DropColumn("dbo.Posts", "CreatorId");
        }
    }
}
