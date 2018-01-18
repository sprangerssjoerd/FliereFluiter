using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FliereFluiter.Domain.Abstract;
using FliereFluiter.Domain.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FliereFluiter.Domain.Concrete
{
    public class EFGuestRepository : IGuestRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Guest> Guests
        {
            get { return context.Guests; }
        }

        public Guest addGuest(Guest guest)
        {

            context.Guests.Add(guest);
            try
            {
                context.SaveChanges();
            }
            catch(SqlException sqlex)
            {
                throw new Exception("guest is not saved", sqlex);
            }
            return guest;

        }

        public Guest getGuestByGuestId(int guestId)
        {
            try
            {
                Guest guest = context.Guests.Single(m => m.Id.Equals(guestId));
                return guest;
            }
            catch(Exception ex)
            {
                throw new Exception("ID not Found", ex);
            }
        }

        public IEnumerable<Guest> getAllGuests()
        {
            try
            {
                IEnumerable<Guest> guests = context.Guests;
                return guests;
            }
            catch(Exception ex)
            {
                throw new Exception("No guests", ex);
            }
        }

    }
}
