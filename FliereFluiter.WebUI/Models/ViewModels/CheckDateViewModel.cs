using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using FliereFluiter.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FliereFluiter.WebUI.Models
{
    public class CheckDateViewModel
    {
        public CampingField campingField {get; set; }
        public ArrayList placeavailability { get; set; }
        public ArrayList placeAv { get; set; }

        public ArrayList placeIdAv { get; set; }
        public ArrayList placeId { get; set; }
        public ArrayList placeNameAv { get; set; }
        public ArrayList placeBoolAv { get; set; }

        public IEnumerable<CampingField> CampingFields { get; set; }
        public IEnumerable<CampingPlace> CampingPlaces { get; set; }

        [DataType(DataType.Date)]
        public DateTime beginDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime endDate { get; set; }
    }
}