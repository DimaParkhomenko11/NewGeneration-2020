namespace Gallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_new_UserPathImages_field_in_TemporaryMedia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TemporaryMedia", "UserPathImages", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TemporaryMedia", "UserPathImages");
        }
    }
}
