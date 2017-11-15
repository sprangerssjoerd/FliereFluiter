using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FliereFluiter.Domain.Entities
{
    [Table("PlaceReservationCampingPlace")]
    public class PlaceReservationCampingPlace
    {
        [Key, Column(Order = 0)]
        public int PlaceReservationId { get; set; }

        [Key, Column(Order = 1)]
        public int CampingPlaceId { get; set; }

        public PlaceReservation PlaceReservation { get; set; }
        public CampingPlace CampingPlace { get; set; }

        public DateTime PeriodBegin { get; set; }
        public DateTime PeriodEnd { get; set; }
    }
}
