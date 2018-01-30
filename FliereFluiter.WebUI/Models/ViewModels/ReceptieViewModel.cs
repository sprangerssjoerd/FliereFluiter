using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using FliereFluiter.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FliereFluiter.WebUI.Models
{
    public class ReceptieViewModel
    {
        public string userName { get; set; }
        public IEnumerable<PlaceReservation> placeReservations { get; set; }
        public PlaceReservation placeReservation { get; set; }
        public IEnumerable<Invoice> invoices { get; set; }
        public Guest guest { get; set; }
        public List<CampingPlace> campingPlaceList { get; set; }
    }
}