using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FliereFluiter.Domain.Entities
{
    [Table("PlaceReservation")]
    public class PlaceReservation
    {
        [Key]
        public int PlaceReservationId { get; set; }

        public virtual Guest Guest { get; set; }

        public int GuestId { get; set; }

        public int GuestAmount { get; set; }
        public int ChildrenAmount { get; set; }
        public bool Discount { get; set; }

        [ForeignKey("PlaceReservationId")]
        public virtual IEnumerable<PlaceReservationFellowGuest> PlaceReservationFellowGuests { get; set; }

        [ForeignKey("PlaceReservationId")]
        public virtual IEnumerable<PlaceReservationCampingPlace> PlaceReservationCampingPlaces { get; set; }
    }
}
