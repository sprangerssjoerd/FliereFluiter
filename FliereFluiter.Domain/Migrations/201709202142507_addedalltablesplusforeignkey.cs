namespace FliereFluiter.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedalltablesplusforeignkey : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Invoice", name: "PlaceReservationFK", newName: "PlaceReservationId");
            RenameIndex(table: "dbo.Invoice", name: "IX_PlaceReservationFK", newName: "IX_PlaceReservationId");
            CreateTable(
                "dbo.CampingField",
                c => new
                    {
                        CampingFieldId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Measurement = c.Int(nullable: false),
                        Amp6 = c.Boolean(nullable: false),
                        Amp10 = c.Boolean(nullable: false),
                        Wifi = c.Boolean(nullable: false),
                        Water = c.Boolean(nullable: false),
                        Riool = c.Boolean(nullable: false),
                        CAI = c.Boolean(nullable: false),
                        price = c.Double(nullable: false),
                        SeasonPrice = c.Double(),
                    })
                .PrimaryKey(t => t.CampingFieldId);
            
            CreateTable(
                "dbo.CampingPlace",
                c => new
                    {
                        CampingPlaceId = c.Int(nullable: false, identity: true),
                        CampingFieldId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CampingPlaceId);
            
            CreateTable(
                "dbo.InvoiceSeason",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false),
                        SeasonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.InvoiceId, t.SeasonId })
                .ForeignKey("dbo.Invoice", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.Season", t => t.SeasonId, cascadeDelete: true)
                .Index(t => t.InvoiceId)
                .Index(t => t.SeasonId);
            
            CreateTable(
                "dbo.Season",
                c => new
                    {
                        SeasonID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.SeasonID);
            
            CreateTable(
                "dbo.PlaceReservationCampingPlace",
                c => new
                    {
                        PlaceReservationId = c.Int(nullable: false),
                        CampingPlaceId = c.Int(nullable: false),
                        PeriodBegin = c.DateTime(nullable: false),
                        PeriodEnd = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlaceReservationId, t.CampingPlaceId })
                .ForeignKey("dbo.CampingPlace", t => t.CampingPlaceId, cascadeDelete: true)
                .ForeignKey("dbo.PlaceReservation", t => t.PlaceReservationId, cascadeDelete: true)
                .Index(t => t.PlaceReservationId)
                .Index(t => t.CampingPlaceId);
            
            CreateTable(
                "dbo.SeasonDate",
                c => new
                    {
                        SeasondateID = c.Int(nullable: false, identity: true),
                        SeasonId = c.Int(nullable: false),
                        PeriodBegin = c.DateTime(nullable: false),
                        periodEnd = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SeasondateID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlaceReservationCampingPlace", "PlaceReservationId", "dbo.PlaceReservation");
            DropForeignKey("dbo.PlaceReservationCampingPlace", "CampingPlaceId", "dbo.CampingPlace");
            DropForeignKey("dbo.InvoiceSeason", "SeasonId", "dbo.Season");
            DropForeignKey("dbo.InvoiceSeason", "InvoiceId", "dbo.Invoice");
            DropIndex("dbo.PlaceReservationCampingPlace", new[] { "CampingPlaceId" });
            DropIndex("dbo.PlaceReservationCampingPlace", new[] { "PlaceReservationId" });
            DropIndex("dbo.InvoiceSeason", new[] { "SeasonId" });
            DropIndex("dbo.InvoiceSeason", new[] { "InvoiceId" });
            DropTable("dbo.SeasonDate");
            DropTable("dbo.PlaceReservationCampingPlace");
            DropTable("dbo.Season");
            DropTable("dbo.InvoiceSeason");
            DropTable("dbo.CampingPlace");
            DropTable("dbo.CampingField");
            RenameIndex(table: "dbo.Invoice", name: "IX_PlaceReservationId", newName: "IX_PlaceReservationFK");
            RenameColumn(table: "dbo.Invoice", name: "PlaceReservationId", newName: "PlaceReservationFK");
        }
    }
}
