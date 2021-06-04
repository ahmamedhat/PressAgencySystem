namespace PressAgencySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingViewCounts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Views", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Views");
        }
    }
}
