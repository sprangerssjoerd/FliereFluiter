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
    public class Facility
    {
        [Key]
        public int FacilityId { get; set; }

        public string Name { get; set; }
        public int Price { get; set; }
        public int Totalamount { get; set; }
        public string Description { get; set; }

        [ForeignKey("FacilityId")]
        public IEnumerable<FacilityReservation> FacilityReservations { get; set; }
    }
}
