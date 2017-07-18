namespace MyMeetings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedchat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PublicationChats",
                c => new
                    {
                        ChatId = c.String(nullable: false, maxLength: 128),
                        Publication_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ChatId)
                .ForeignKey("dbo.Publications", t => t.Publication_Id)
                .Index(t => t.Publication_Id);
            
            CreateTable(
                "dbo.ChatMessages",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        Text = c.String(),
                        User_UserId = c.String(maxLength: 128),
                        PublicationChat_ChatId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.ChatUsers", t => t.User_UserId)
                .ForeignKey("dbo.PublicationChats", t => t.PublicationChat_ChatId)
                .Index(t => t.User_UserId)
                .Index(t => t.PublicationChat_ChatId);
            
            CreateTable(
                "dbo.ChatUsers",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginTime = c.DateTime(nullable: false),
                        LastPing = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        PublicationChat_ChatId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.PublicationChats", t => t.PublicationChat_ChatId)
                .Index(t => t.User_Id)
                .Index(t => t.PublicationChat_ChatId);
            
            AddColumn("dbo.Publications", "Chat_ChatId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Publications", "Chat_ChatId");
            AddForeignKey("dbo.Publications", "Chat_ChatId", "dbo.PublicationChats", "ChatId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChatUsers", "PublicationChat_ChatId", "dbo.PublicationChats");
            DropForeignKey("dbo.Publications", "Chat_ChatId", "dbo.PublicationChats");
            DropForeignKey("dbo.PublicationChats", "Publication_Id", "dbo.Publications");
            DropForeignKey("dbo.ChatMessages", "PublicationChat_ChatId", "dbo.PublicationChats");
            DropForeignKey("dbo.ChatMessages", "User_UserId", "dbo.ChatUsers");
            DropForeignKey("dbo.ChatUsers", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Publications", new[] { "Chat_ChatId" });
            DropIndex("dbo.ChatUsers", new[] { "PublicationChat_ChatId" });
            DropIndex("dbo.ChatUsers", new[] { "User_Id" });
            DropIndex("dbo.ChatMessages", new[] { "PublicationChat_ChatId" });
            DropIndex("dbo.ChatMessages", new[] { "User_UserId" });
            DropIndex("dbo.PublicationChats", new[] { "Publication_Id" });
            DropColumn("dbo.Publications", "Chat_ChatId");
            DropTable("dbo.ChatUsers");
            DropTable("dbo.ChatMessages");
            DropTable("dbo.PublicationChats");
        }
    }
}
