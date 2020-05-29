namespace Gallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Renamed_the_UniqueIdentificator_field_to_UniqueIdentName_in_TemporaryMedia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TemporaryMedia", "UniqueIdentName", c => c.String(nullable: false));
            DropColumn("dbo.TemporaryMedia", "UniqueIdentificator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TemporaryMedia", "UniqueIdentificator", c => c.String(nullable: false));
            DropColumn("dbo.TemporaryMedia", "UniqueIdentName");
        }
    }
}
