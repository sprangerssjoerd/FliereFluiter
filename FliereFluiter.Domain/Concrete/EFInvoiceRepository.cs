using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FliereFluiter.Domain.Abstract;
using FliereFluiter.Domain.Entities;
using System.Collections.Generic;

namespace FliereFluiter.Domain.Concrete
{
    public class EFInvoiceRepository : IInvoiceRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Invoice> Invoices
        {
            get { return context.Invoices; }
        }

        public Invoice getInvoicesByPlaceReservationId(int PRId)
        {
            Invoice invoice = new Invoice();
            try
            {
                invoice = context.Invoices.Single(m => m.PlaceReservationId.Equals(PRId));
                return invoice;
            }
            catch
            {
                return invoice = null;
            }
        }
    }
}
