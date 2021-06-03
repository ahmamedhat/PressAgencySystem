namespace PressAgencySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingArticleTypeToEveryPost : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Posts", "ArticalTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "ArticleType_Id", c => c.Int());
            CreateIndex("dbo.Posts", "ArticleType_Id");
            AddForeignKey("dbo.Posts", "ArticleType_Id", "dbo.ArticleTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "ArticleType_Id", "dbo.ArticleTypes");
            DropIndex("dbo.Posts", new[] { "ArticleType_Id" });
            DropColumn("dbo.Posts", "ArticleType_Id");
            DropColumn("dbo.Posts", "ArticalTypeId");
            DropTable("dbo.ArticleTypes");
        }
    }
}
