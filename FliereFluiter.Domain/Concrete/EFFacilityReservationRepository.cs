﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FliereFluiter.Domain.Abstract;
using FliereFluiter.Domain.Entities;
using System.Collections.Generic;

namespace FliereFluiter.Domain.Concrete
{
    public class EFFacilityReservationRepository : IFacilityReservationRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<FacilityReservation> FacilityReservations
        {
            get { return context.FacilityReservations; }
        }
    }
}
