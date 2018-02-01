using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FliereFluiter.Domain.Entities;

namespace FliereFluiter.Domain.Abstract
{
    public interface IPlaceReservationCampingPlaceRepository
    {
        IEnumerable<PlaceReservationCampingPlace> PlaceReservationCampingPlaces { get; }
        IEnumerable<PlaceReservationCampingPlace> getReservationsPlaceByCampingPlaceId(int PlaceId);
        PlaceReservationCampingPlace addPRCP(PlaceReservationCampingPlace prcp);
        IEnumerable<PlaceReservationCampingPlace> getPRCPByCampingPlaceId(int id);
        IEnumerable<PlaceReservationCampingPlace> getPRCPs();
        IEnumerable<PlaceReservationCampingPlace> getPRCPByPlaceReservationId(int id);
        void UpdatePRCP(PlaceReservationCampingPlace prcp);
    }
}