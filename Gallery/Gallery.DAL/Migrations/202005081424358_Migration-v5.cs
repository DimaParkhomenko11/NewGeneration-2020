namespace Gallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrationv5 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Attempts", newName: "Attempt");
            RenameTable(name: "dbo.MediaTypes", newName: "MediaType");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.MediaType", newName: "MediaTypes");
            RenameTable(name: "dbo.Attempt", newName: "Attempts");
        }
    }
}
