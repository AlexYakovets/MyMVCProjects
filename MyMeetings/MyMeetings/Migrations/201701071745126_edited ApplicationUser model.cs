namespace MyMeetings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedApplicationUsermodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "SurName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Gender", c => c.String());
            AddColumn("dbo.AspNetUsers", "DateOfRegistration", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "DateOfBirth", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "DateOfBirth", c => c.DateTime());
            DropColumn("dbo.AspNetUsers", "DateOfRegistration");
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "SurName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
