using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FliereFluiter.Domain.Abstract;
using FliereFluiter.Domain.Entities;
using System.Collections.Generic;

namespace FliereFluiter.Domain.Concrete
{
    public class EFSeasonDateRepository : ISeasonDateRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<SeasonDate> SeasonDates
        {
            get { return context.SeasonDates; }
        }
    }
}
