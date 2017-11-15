using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FliereFluiter.Domain.Entities
{
    [Table("CampingPlace")]
    public class CampingPlace
    {
        [Key, Column(Order = 0)]
        public int CampingPlaceId { get; set; }

       
        //foreign key
        public int CampingFieldId { get; set; }
        //navigation property
        public virtual CampingField CampingField { get; set; }

        public string Name { get; set; }

        [ForeignKey("CampingPlaceId")]
        public virtual IEnumerable<PlaceReservationCampingPlace> PlaceReservationCampingPlaces { get; set; }
    }
}
