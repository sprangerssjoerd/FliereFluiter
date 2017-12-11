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
            return context.Invoices.Single(m => m.PlaceReservationId.Equals(PRId));
        }
    }
}
