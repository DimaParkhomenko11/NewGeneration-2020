namespace Gallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrationv4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Attempts", "TimeStamp", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Attempts", "TimeStamp", c => c.DateTime(nullable: false));
        }
    }
}
