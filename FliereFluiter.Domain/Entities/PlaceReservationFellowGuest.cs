using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FliereFluiter.Domain.Entities
{
    [Table("PlaceReservationFellowGuest")]
    public class PlaceReservationFellowGuest
    {
        [Key, Column(Order = 0)]
        public int FellowGuestId { get; set; }
        [Key, Column(Order = 1)]
        public int PlaceReservationId { get; set; }

        public PlaceReservation PlaceReservation { get; set; }
        public FellowGuest FellowGuest { get; set; }
    }
}
