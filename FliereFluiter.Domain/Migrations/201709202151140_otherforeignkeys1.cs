namespace FliereFluiter.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class otherforeignkeys1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.SeasonDate", "SeasonId");
            AddForeignKey("dbo.SeasonDate", "SeasonId", "dbo.Season", "SeasonID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SeasonDate", "SeasonId", "dbo.Season");
            DropIndex("dbo.SeasonDate", new[] { "SeasonId" });
        }
    }
}
