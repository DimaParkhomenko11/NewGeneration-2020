namespace Gallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_changes_to_Media_table : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Media", "PathToMedia", c => c.String(nullable: false, maxLength: 500));
            CreateIndex("dbo.Media", "PathToMedia", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Media", new[] { "PathToMedia" });
            AlterColumn("dbo.Media", "PathToMedia", c => c.String(maxLength: 8000, unicode: false));
        }
    }
}
