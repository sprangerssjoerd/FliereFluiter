﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FliereFluiter.Domain.Entities;

namespace FliereFluiter.Domain.Abstract
{
    public interface IPlaceReservationFellowGuestRepository
    {
        IEnumerable<PlaceReservationFellowGuest> PlaceReservationFellowGuests { get; }
    }

}
