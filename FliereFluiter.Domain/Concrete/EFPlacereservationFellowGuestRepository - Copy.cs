using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FliereFluiter.Domain.Abstract;
using FliereFluiter.Domain.Entities;
using System.Collections.Generic;

namespace FliereFluiter.Domain.Concrete
{
    public class EFPlaceReservationFellowGuestRepository : IPlaceReservationFellowGuestRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<PlaceReservationFellowGuest> PlaceReservationFellowGuests
        {
            get { return context.PlaceReservationsellowGuests; }
        }
    }
}
