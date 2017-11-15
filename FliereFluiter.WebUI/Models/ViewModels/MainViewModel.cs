using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FliereFluiter.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FliereFluiter.WebUI.Models
{
    public class MainViewModel
    {
        public CampingField campingField;
        public Guest guest;

        [DataType(DataType.Date)]
        public DateTime birthday { get; set; }

        [DataType(DataType.Date)]
        public DateTime beginDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime endDate { get; set; }

        public int fieldId { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public IEnumerable<Guest> Guests { get; set; }
        public IEnumerable<CampingField> CampingFields { get; set; }
        public IEnumerable<CampingPlace> CampingPlaces { get; set; }
        public IEnumerable<PlaceReservationCampingPlace> PlaceReservationCampingPlaces { get; set; }
    }

}