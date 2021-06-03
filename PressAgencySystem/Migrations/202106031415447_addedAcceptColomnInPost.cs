namespace PressAgencySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedAcceptColomnInPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Accepted", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Accepted");
        }
    }
}
