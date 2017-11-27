using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FliereFluiter.Domain.Entities;
using System.Data.Entity;

namespace FliereFluiter.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Guest> Guests { get; set; }
        public DbSet<PlaceReservation> PlaceReservations { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<FacilityReservation> FacilityReservations { get; set; }
        public DbSet<FellowGuest> FellowGuests { get; set; }
        public DbSet<PlaceReservationFellowGuest> PlaceReservationsellowGuests { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<SeasonDate> SeasonDates { get; set; }
        public DbSet<InvoiceSeason> InvoiceSeasons { get; set; }
        public DbSet<CampingField> CampingFields { get; set; }
        public DbSet<CampingPlace> CampingPlaces { get; set; }
        public DbSet<PlaceReservationCampingPlace> PlaceReservationCampingPlaces { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInformation> UserInformations { get; set; }
    }
}
