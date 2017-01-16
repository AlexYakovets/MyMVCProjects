namespace MyMeetings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editsinmodelwithsubscribers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Publication_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "Publication_Id");
            AddForeignKey("dbo.AspNetUsers", "Publication_Id", "dbo.Publications", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Publication_Id", "dbo.Publications");
            DropIndex("dbo.AspNetUsers", new[] { "Publication_Id" });
            DropColumn("dbo.AspNetUsers", "Publication_Id");
        }
    }
}
