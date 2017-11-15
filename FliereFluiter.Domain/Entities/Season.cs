using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FliereFluiter.Domain.Entities
{
    [Table("Season")]
    public class Season
    {
        [Key, Column(Order = 0)]
        public int SeasonID { get; set; }

        public string Name { get; set; }
        public double Price { get; set; }

        [ForeignKey("SeasonId")]
        public virtual IEnumerable<InvoiceSeason> InvoiceSeasons { get; set; }

        [ForeignKey("SeasonId")]
        public virtual IEnumerable<SeasonDate> SeasonDates { get; set; }
    }
}
