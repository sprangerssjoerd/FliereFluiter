using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FliereFluiter.Domain.Entities
{
    [Table("SeasonDate")]
    public class SeasonDate
    {
        [Key, Column(Order = 0)]
        public int SeasondateID { get; set; }

        public virtual Season Season { get; set; }

        public int SeasonId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PeriodBegin { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime periodEnd { get; set; }
    }
}
