namespace MyMeetings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class homemigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PublicationCategories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Publications", "Category_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Publications", "Category_Id");
            AddForeignKey("dbo.Publications", "Category_Id", "dbo.PublicationCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Publications", "Category_Id", "dbo.PublicationCategories");
            DropIndex("dbo.Publications", new[] { "Category_Id" });
            DropColumn("dbo.Publications", "Category_Id");
            DropTable("dbo.PublicationCategories");
        }
    }
}
