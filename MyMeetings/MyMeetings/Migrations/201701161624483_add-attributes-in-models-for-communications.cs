namespace MyMeetings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addattributesinmodelsforcommunications : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Publications", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Publication_Id", "dbo.Publications");
            DropIndex("dbo.Publications", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Publications", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.AspNetUsers", new[] { "Publication_Id" });
            DropColumn("dbo.Publications", "AuthorId");
            RenameColumn(table: "dbo.Publications", name: "ApplicationUser_Id1", newName: "AuthorId");
            CreateTable(
                "dbo.PublicationApplicationUsers",
                c => new
                    {
                        Publication_Id = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Publication_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.Publications", t => t.Publication_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Publication_Id)
                .Index(t => t.ApplicationUser_Id);
            
            DropColumn("dbo.Publications", "ApplicationUser_Id");
            DropColumn("dbo.AspNetUsers", "Publication_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Publication_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Publications", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.PublicationApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PublicationApplicationUsers", "Publication_Id", "dbo.Publications");
            DropIndex("dbo.PublicationApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.PublicationApplicationUsers", new[] { "Publication_Id" });
            DropTable("dbo.PublicationApplicationUsers");
            RenameColumn(table: "dbo.Publications", name: "AuthorId", newName: "ApplicationUser_Id1");
            AddColumn("dbo.Publications", "AuthorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "Publication_Id");
            CreateIndex("dbo.Publications", "ApplicationUser_Id1");
            CreateIndex("dbo.Publications", "ApplicationUser_Id");
            AddForeignKey("dbo.AspNetUsers", "Publication_Id", "dbo.Publications", "Id");
            AddForeignKey("dbo.Publications", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
