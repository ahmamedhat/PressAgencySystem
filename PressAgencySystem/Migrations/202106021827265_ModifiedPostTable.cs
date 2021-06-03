namespace PressAgencySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedPostTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "ArticalTypeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "ArticalTypeId");
        }
    }
}
