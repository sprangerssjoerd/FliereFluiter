using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FliereFluiter.Domain.Abstract;
using FliereFluiter.Domain.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FliereFluiter.Domain.Concrete
{
    public class EFPlaceReservationRepository : IPlaceReservationRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<PlaceReservation> PlaceReservations
        {
            get { return context.PlaceReservations; }
        }

        public PlaceReservation addPlaceReservation(PlaceReservation placeReservation)
        {

            context.PlaceReservations.Add(placeReservation);
            try
            {
                context.SaveChanges();
            }
            catch (SqlException sqlex)
            {
                throw new Exception("PlaceReservation is not saved", sqlex);
            }
            return placeReservation;

        }
        public PlaceReservation getPlaceReservationById(int placeReservationId)
        {
            PlaceReservation placeReservation = context.PlaceReservations.Single(m => m.PlaceReservationId.Equals(placeReservationId));
            return placeReservation;
        }


        public IEnumerable<PlaceReservation> getPlaceReservationsWhereDefIsFalse()
        {
            IEnumerable<PlaceReservation> placeReservations = context.PlaceReservations.Where(m => m.DefReservation.Equals(!true));
            return placeReservations;
        }

        public void setDefRes(PlaceReservation placeRes)
        {
            placeRes.DefReservation = true;
            context.SaveChanges();
        }

        public IEnumerable<PlaceReservation> getPlaceReservationByGuestId(int guestId)
        {
            IEnumerable<PlaceReservation> PRList = context.PlaceReservations.Where(m => m.GuestId.Equals(guestId));
            return PRList;
        }
    }
}
