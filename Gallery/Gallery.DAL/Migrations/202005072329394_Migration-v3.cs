namespace Gallery.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrationv3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attempts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeStamp = c.DateTime(nullable: false),
                        Success = c.Boolean(nullable: false),
                        IpAddress = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 40, unicode: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attempts", "UserId", "dbo.Users");
            DropIndex("dbo.Attempts", new[] { "UserId" });
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 25, unicode: false));
            DropTable("dbo.Attempts");
        }
    }
}
