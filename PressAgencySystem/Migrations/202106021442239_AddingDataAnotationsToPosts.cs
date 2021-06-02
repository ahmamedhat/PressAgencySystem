namespace PressAgencySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDataAnotationsToPosts : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Posts", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Description", c => c.String());
            AlterColumn("dbo.Posts", "Title", c => c.String());
        }
    }
}
