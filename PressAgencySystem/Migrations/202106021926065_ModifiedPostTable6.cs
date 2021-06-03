namespace PressAgencySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedPostTable6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "ArticleType_Id", "dbo.ArticleTypes");
            DropIndex("dbo.Posts", new[] { "ArticleType_Id" });
            RenameColumn(table: "dbo.Posts", name: "ArticleType_Id", newName: "ArticleTypeId");
            AlterColumn("dbo.Posts", "ArticleTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "ArticleTypeId");
            AddForeignKey("dbo.Posts", "ArticleTypeId", "dbo.ArticleTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "ArticleTypeId", "dbo.ArticleTypes");
            DropIndex("dbo.Posts", new[] { "ArticleTypeId" });
            AlterColumn("dbo.Posts", "ArticleTypeId", c => c.Int());
            RenameColumn(table: "dbo.Posts", name: "ArticleTypeId", newName: "ArticleType_Id");
            CreateIndex("dbo.Posts", "ArticleType_Id");
            AddForeignKey("dbo.Posts", "ArticleType_Id", "dbo.ArticleTypes", "Id");
        }
    }
}
