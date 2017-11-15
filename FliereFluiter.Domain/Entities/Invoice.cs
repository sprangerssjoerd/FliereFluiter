using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FliereFluiter.Domain.Entities;

namespace FliereFluiter.Domain.Entities
{

    [Table("Invoice")]
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }

        [ForeignKey("PlaceReservation")]
        public int PlaceReservationId { get; set; }

        public DateTime Invoice_Date { get; set; }

        public PlaceReservation PlaceReservation { get; set; }

        [ForeignKey("InvoiceId")]
        public virtual IEnumerable<InvoiceSeason> InvoiceSeasons { get; set; }

    }
}
