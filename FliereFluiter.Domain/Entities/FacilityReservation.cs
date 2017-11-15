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
    [Table("FacilityReservation")]
    public class FacilityReservation
    {
        [Key]
        public int FacilityReservationId { get; set; }

        public virtual Guest Guest { get; set; }
        public virtual Facility Facility { get; set; }

        public int GuestId { get; set; }
        public int FacilityId { get; set; }

        public DateTime ReservationBeginDate { get; set; }
        public DateTime ReservationEndDate { get; set; }
        public int ReservationAmount { get; set; }
    }
}
