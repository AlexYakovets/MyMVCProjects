namespace MyMeetings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMyMeetMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DateOfBirth", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DateOfBirth");
        }
    }
}
