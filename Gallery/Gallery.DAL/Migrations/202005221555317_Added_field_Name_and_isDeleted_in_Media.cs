namespace Gallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_field_Name_and_isDeleted_in_Media : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Media", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Media", "Name");
        }
    }
}
