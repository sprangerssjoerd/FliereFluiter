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

        public IEnumerable<SeasonDate> getRelevantSeasonDates(DateTime begin, DateTime end)
        {
            IEnumerable<SeasonDate> seasonDates;

            try
            {
                seasonDates = context.SeasonDates.Where(m => m.periodEnd > end && m.PeriodBegin < end);
                return seasonDates;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("no dates have been found");
                return null;
            }
        }
    }
}
