namespace MyMeetings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editsinPublicationmodel : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Publications", new[] { "Author_Id" });
            DropColumn("dbo.Publications", "AuthorId");
            RenameColumn(table: "dbo.Publications", name: "Author_Id", newName: "AuthorId");
            AddColumn("dbo.Publications", "DateTimeOfPublication", c => c.DateTime(nullable: false));
            AddColumn("dbo.Publications", "DateOfMeeting", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Publications", "AuthorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Publications", "AuthorId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Publications", new[] { "AuthorId" });
            AlterColumn("dbo.Publications", "AuthorId", c => c.Int(nullable: false));
            DropColumn("dbo.Publications", "DateOfMeeting");
            DropColumn("dbo.Publications", "DateTimeOfPublication");
            RenameColumn(table: "dbo.Publications", name: "AuthorId", newName: "Author_Id");
            AddColumn("dbo.Publications", "AuthorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Publications", "Author_Id");
        }
    }
}
