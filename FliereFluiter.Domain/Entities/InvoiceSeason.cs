using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FliereFluiter.Domain.Entities
{
    [Table("InvoiceSeason")]
    public class InvoiceSeason
    {
        [Key, Column(Order = 0)]
        public int InvoiceId { get; set; }
        
        [Key, Column(Order = 1)]
        public int SeasonId { get; set; }

        public Invoice Invoice { get; set; }
        public Season Season { get; set; }
    }
}
