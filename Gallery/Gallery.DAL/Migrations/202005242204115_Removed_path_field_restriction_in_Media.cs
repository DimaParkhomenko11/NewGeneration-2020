namespace Gallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removed_path_field_restriction_in_Media : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Media", "PathToMedia", c => c.String(maxLength: 8000, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Media", "PathToMedia", c => c.String(maxLength: 25, unicode: false));
        }
    }
}
