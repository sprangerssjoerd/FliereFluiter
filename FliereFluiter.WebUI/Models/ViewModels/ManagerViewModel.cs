using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using FliereFluiter.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FliereFluiter.WebUI.Models
{
    public class ManagerViewModel
    {
        //from ReceptieViewModel
        public string userName { get; set; }
        public IEnumerable<PlaceReservation> placeReservations { get; set; }
        public PlaceReservation placeReservation { get; set; }
        public IEnumerable<Invoice> invoices { get; set; }

        //part from ManagerViewModel
        public string placeName { get; set; }
        public List<string> occupancyRate { get; set; }
        public IEnumerable<CampingPlace> campingPlaceList { get; set; }
        public List<string> campingnameList { get; set; }
        public List<int> idList { get; set; }
        public IEnumerable<Guest> guestList { get; set; }
        public List<string> bookingStrings { get; set; }


    }
}