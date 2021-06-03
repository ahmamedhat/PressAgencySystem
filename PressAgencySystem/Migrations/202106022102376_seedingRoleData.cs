namespace PressAgencySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedingRoleData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Roles(Title) VALUES('Admin') ");
            Sql("INSERT INTO Roles(Title) VALUES('Editor') ");
            Sql("INSERT INTO Roles(Title) VALUES('Viewer') ");

        }

        public override void Down()
        {
        }
    }
}
