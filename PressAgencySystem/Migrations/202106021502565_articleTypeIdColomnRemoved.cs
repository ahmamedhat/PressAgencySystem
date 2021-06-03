namespace PressAgencySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class articleTypeIdColomnRemoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Posts", "ArticalTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "ArticalTypeId", c => c.Int(nullable: false));
        }
    }
}
