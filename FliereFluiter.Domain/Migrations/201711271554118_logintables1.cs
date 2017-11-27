namespace FliereFluiter.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class logintables1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.UserInformation",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserInformation", "RoleId", "dbo.Role");
            DropIndex("dbo.UserInformation", new[] { "RoleId" });
            DropTable("dbo.UserInformation");
            DropTable("dbo.Role");
        }
    }
}
