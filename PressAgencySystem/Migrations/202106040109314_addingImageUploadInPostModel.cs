namespace PressAgencySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingImageUploadInPostModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "ImagePath");
        }
    }
}
