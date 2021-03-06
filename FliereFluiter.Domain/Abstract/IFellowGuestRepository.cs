﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FliereFluiter.Domain.Entities;

namespace FliereFluiter.Domain.Abstract
{
    public interface IFellowGuestRepository
    {
        IEnumerable<FellowGuest> FellowGuests { get; }
    }
}
