namespace PressAgencySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingUserRolesToUsersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "RoleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "RoleId");
            AddForeignKey("dbo.Users", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropColumn("dbo.Users", "RoleId");
        }
    }
}
