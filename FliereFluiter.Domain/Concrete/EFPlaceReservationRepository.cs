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
    }
}
