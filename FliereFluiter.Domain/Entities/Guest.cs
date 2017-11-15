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
    [Table("Guests")]
    public class Guest
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Postalcode { get; set; }
        public string City { get; set; }
        public string Telnumber { get; set; }
        public string Mobnumber { get; set; }
        public DateTime Birthday { get; set; }
        public string Socialnumber { get; set; }
        public string Socialcard { get; set; }

        [ForeignKey("GuestId")]
        public virtual IEnumerable<PlaceReservation> PlaceReservations { get; set; }

        [ForeignKey("GuestId")]
        public virtual IEnumerable<FacilityReservation> FacilityReservations { get; set; }
    }
}
