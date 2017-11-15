using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FliereFluiter.Domain.Entities
{
	[Table("FellowGuest")]
    public class FellowGuest
    {
		[Key]
		public int FellowGuestId { get; set; }

		public string Name { get; set; }
		public DateTime Birthday { get; set; }

        [ForeignKey("FellowGuestId")]
        public virtual IEnumerable<PlaceReservationFellowGuest> PlaceReservationFellowGuests { get; set; }
    }
}
