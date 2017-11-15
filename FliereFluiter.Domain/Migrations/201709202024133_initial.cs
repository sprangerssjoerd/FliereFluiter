namespace FliereFluiter.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Facilities",
                c => new
                    {
                        FacilityId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                        Totalamount = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.FacilityId);
            
            CreateTable(
                "dbo.FacilityReservation",
                c => new
                    {
                        FacilityReservationId = c.Int(nullable: false, identity: true),
                        GuestId = c.Int(nullable: false),
                        FacilityId = c.Int(nullable: false),
                        ReservationBeginDate = c.DateTime(nullable: false),
                        ReservationEndDate = c.DateTime(nullable: false),
                        ReservationAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FacilityReservationId)
                .ForeignKey("dbo.Facilities", t => t.FacilityId, cascadeDelete: true)
                .ForeignKey("dbo.Guests", t => t.GuestId, cascadeDelete: true)
                .Index(t => t.GuestId)
                .Index(t => t.FacilityId);
            
            CreateTable(
                "dbo.Guests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Adress = c.String(),
                        Postalcode = c.String(),
                        City = c.String(),
                        Telnumber = c.String(),
                        Mobnumber = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Socialnumber = c.String(),
                        Socialcard = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FellowGuest",
                c => new
                    {
                        FellowGuestId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Birthday = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FellowGuestId);
            
            CreateTable(
                "dbo.Invoice",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false, identity: true),
                        PlaceReservationFK = c.Int(nullable: false),
                        Invoice_Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.InvoiceId)
                .ForeignKey("dbo.PlaceReservation", t => t.PlaceReservationFK, cascadeDelete: true)
                .Index(t => t.PlaceReservationFK);
            
            CreateTable(
                "dbo.PlaceReservation",
                c => new
                    {
                        PlaceReservationId = c.Int(nullable: false, identity: true),
                        GuestFk = c.Int(nullable: false),
                        GuestAmount = c.Int(nullable: false),
                        ChildrenAmount = c.Int(nullable: false),
                        Discount = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PlaceReservationId);
            
            CreateTable(
                "dbo.PlaceReservationFellowGuest",
                c => new
                    {
                        FellowGuestId = c.Int(nullable: false),
                        PlaceReservationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FellowGuestId, t.PlaceReservationId })
                .ForeignKey("dbo.FellowGuest", t => t.FellowGuestId, cascadeDelete: true)
                .ForeignKey("dbo.PlaceReservation", t => t.PlaceReservationId, cascadeDelete: true)
                .Index(t => t.FellowGuestId)
                .Index(t => t.PlaceReservationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlaceReservationFellowGuest", "PlaceReservationId", "dbo.PlaceReservation");
            DropForeignKey("dbo.PlaceReservationFellowGuest", "FellowGuestId", "dbo.FellowGuest");
            DropForeignKey("dbo.Invoice", "PlaceReservationFK", "dbo.PlaceReservation");
            DropForeignKey("dbo.FacilityReservation", "GuestId", "dbo.Guests");
            DropForeignKey("dbo.FacilityReservation", "FacilityId", "dbo.Facilities");
            DropIndex("dbo.PlaceReservationFellowGuest", new[] { "PlaceReservationId" });
            DropIndex("dbo.PlaceReservationFellowGuest", new[] { "FellowGuestId" });
            DropIndex("dbo.Invoice", new[] { "PlaceReservationFK" });
            DropIndex("dbo.FacilityReservation", new[] { "FacilityId" });
            DropIndex("dbo.FacilityReservation", new[] { "GuestId" });
            DropTable("dbo.PlaceReservationFellowGuest");
            DropTable("dbo.PlaceReservation");
            DropTable("dbo.Invoice");
            DropTable("dbo.FellowGuest");
            DropTable("dbo.Guests");
            DropTable("dbo.FacilityReservation");
            DropTable("dbo.Facilities");
        }
    }
}
