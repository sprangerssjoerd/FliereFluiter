﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FliereFluiter.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FliereFluiter.WebUI.Models
{
    public class SuccesFullReservationViewModel
    {
        public Guest guest;
        public PlaceReservation placeReservation;
        public PlaceReservationCampingPlace placeReservationCampingPlace;
    }

}