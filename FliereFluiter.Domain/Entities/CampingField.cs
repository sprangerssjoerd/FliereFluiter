using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FliereFluiter.Domain.Entities
{
    [Table("CampingField")]
    public class CampingField
    {

        [Key, Column(Order = 0)]
        public int CampingFieldId { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }
        public int Measurement { get; set; }
        public bool Amp6 { get; set; }
        public bool Amp10 { get; set; }
        public bool Wifi { get; set; }
        public bool Water { get; set; }
        public bool Riool { get; set; }
        public bool CAI { get; set; }
        public double price { get; set; }
        public double? SeasonPrice { get; set; }

        [ForeignKey("CampingFieldId")]
        public virtual IEnumerable<CampingPlace> CampingPlaces { get; set; }
    }


    public class PlaceAv
    {
        public int PlaceId { get; set; }
        public string Name { get; set; }
        public bool IsTaken { get; set; }
    }
}
