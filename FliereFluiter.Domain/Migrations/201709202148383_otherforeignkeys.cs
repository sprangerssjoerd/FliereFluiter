namespace FliereFluiter.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class otherforeignkeys : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlaceReservation", "GuestId", c => c.Int(nullable: false));
            CreateIndex("dbo.CampingPlace", "CampingFieldId");
            CreateIndex("dbo.PlaceReservation", "GuestId");
            AddForeignKey("dbo.CampingPlace", "CampingFieldId", "dbo.CampingField", "CampingFieldId", cascadeDelete: true);
            AddForeignKey("dbo.PlaceReservation", "GuestId", "dbo.Guests", "Id", cascadeDelete: true);
            DropColumn("dbo.PlaceReservation", "GuestFk");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlaceReservation", "GuestFk", c => c.Int(nullable: false));
            DropForeignKey("dbo.PlaceReservation", "GuestId", "dbo.Guests");
            DropForeignKey("dbo.CampingPlace", "CampingFieldId", "dbo.CampingField");
            DropIndex("dbo.PlaceReservation", new[] { "GuestId" });
            DropIndex("dbo.CampingPlace", new[] { "CampingFieldId" });
            DropColumn("dbo.PlaceReservation", "GuestId");
        }
    }
}
