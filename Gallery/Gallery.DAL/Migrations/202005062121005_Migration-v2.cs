namespace Gallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrationv2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RoleUsers", newName: "UserRoles");
            DropPrimaryKey("dbo.UserRoles");
            AlterColumn("dbo.Media", "PathToMedia", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.MediaTypes", "Type", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 25, unicode: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Roles", "Name", c => c.String(maxLength: 30, unicode: false));
            AddPrimaryKey("dbo.UserRoles", new[] { "User_Id", "Role_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserRoles");
            AlterColumn("dbo.Roles", "Name", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.MediaTypes", "Type", c => c.String());
            AlterColumn("dbo.Media", "PathToMedia", c => c.String());
            AddPrimaryKey("dbo.UserRoles", new[] { "Role_Id", "User_Id" });
            RenameTable(name: "dbo.UserRoles", newName: "RoleUsers");
        }
    }
}
