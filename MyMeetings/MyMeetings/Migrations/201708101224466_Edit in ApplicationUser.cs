namespace MyMeetings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditinApplicationUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "LastActivity", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "Lastlogin", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Lastlogin", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "LastActivity", c => c.DateTime(nullable: false));
        }
    }
}
