namespace MyMeetings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class someeditsinidentityModel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Publications", name: "ApplicationUser_Id", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Publications", name: "ApplicationUser_Id1", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Publications", name: "__mig_tmp__0", newName: "ApplicationUser_Id1");
            RenameIndex(table: "dbo.Publications", name: "IX_ApplicationUser_Id1", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.Publications", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUser_Id1");
            RenameIndex(table: "dbo.Publications", name: "__mig_tmp__0", newName: "IX_ApplicationUser_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Publications", name: "IX_ApplicationUser_Id", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.Publications", name: "IX_ApplicationUser_Id1", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Publications", name: "__mig_tmp__0", newName: "IX_ApplicationUser_Id1");
            RenameColumn(table: "dbo.Publications", name: "ApplicationUser_Id1", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Publications", name: "ApplicationUser_Id", newName: "ApplicationUser_Id1");
            RenameColumn(table: "dbo.Publications", name: "__mig_tmp__0", newName: "ApplicationUser_Id");
        }
    }
}
