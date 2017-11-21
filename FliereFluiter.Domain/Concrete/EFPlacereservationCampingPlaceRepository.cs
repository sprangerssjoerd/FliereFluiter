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
    public class EFPlaceReservationCampingPlaceRepository : IPlaceReservationCampingPlaceRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<PlaceReservationCampingPlace> PlaceReservationCampingPlaces
        {
            get { return context.PlaceReservationCampingPlaces; }
        }

        public IEnumerable<PlaceReservationCampingPlace> getReservationsPlaceByCampingPlaceId(int PlaceId)
        {
            return context.PlaceReservationCampingPlaces.Where(m => m.CampingPlaceId.Equals(PlaceId));
        }

        public PlaceReservationCampingPlace addPRCP(PlaceReservationCampingPlace prcp)
        {

            context.PlaceReservationCampingPlaces.Add(prcp);
            try
            {
                context.SaveChanges();
            }
            catch (SqlException sqlex)
            {
                throw new Exception("PlaceReservationCampingPlace is not saved", sqlex);
            }
            return prcp;

        }
    }
}
