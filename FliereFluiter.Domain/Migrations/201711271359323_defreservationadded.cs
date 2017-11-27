namespace FliereFluiter.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class defreservationadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlaceReservation", "DefReservation", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlaceReservation", "DefReservation");
        }
    }
}
