namespace MyMeetings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedtomodelsfunctionalwithsubscribing : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Publications", "AuthorId", "dbo.AspNetUsers");
            AddColumn("dbo.Publications", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Publications", "ApplicationUser_Id1", c => c.String(maxLength: 128));
            CreateIndex("dbo.Publications", "ApplicationUser_Id");
            CreateIndex("dbo.Publications", "ApplicationUser_Id1");
            AddForeignKey("dbo.Publications", "ApplicationUser_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Publications", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Publications", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Publications", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropIndex("dbo.Publications", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.Publications", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Publications", "ApplicationUser_Id1");
            DropColumn("dbo.Publications", "ApplicationUser_Id");
            AddForeignKey("dbo.Publications", "AuthorId", "dbo.AspNetUsers", "Id");
        }
    }
}
