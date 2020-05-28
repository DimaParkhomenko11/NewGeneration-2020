namespace Gallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New_TemporaryMedia_table_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TemporaryMedia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UniqueIdentificator = c.String(nullable: false),
                        InDuringLoading = c.Boolean(nullable: false),
                        IsSuccess = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TemporaryMedia", "UserId", "dbo.Users");
            DropIndex("dbo.TemporaryMedia", new[] { "UserId" });
            DropTable("dbo.TemporaryMedia");
        }
    }
}
