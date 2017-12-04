namespace FliereFluiter.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rolelvl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Role", "roleLvl", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Role", "roleLvl");
        }
    }
}
