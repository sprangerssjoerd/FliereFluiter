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

        public IEnumerable<SeasonDate> getSeasonDates()
        {
            try
            {
                IEnumerable<SeasonDate> seasonDates = context.SeasonDates;
                return seasonDates;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("no dates have been found");
                return null;
            }
        }

        public SeasonDate getSeasonDateBySeasonId(int seasonId)
        {
            try
            {
                SeasonDate seasonDate = context.SeasonDates.Single(m => m.SeasonId.Equals(seasonId));
                return seasonDate;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("no dates have been found");
                return null;
            }
        }

        public void UpdateSeasonDate(SeasonDate seasonDate)
        {
            SeasonDate result = context.SeasonDates.Single(x => x.SeasondateID.Equals(seasonDate.SeasondateID));
            if (result != null)
            {
                result.PeriodBegin = seasonDate.PeriodBegin;
                result.periodEnd = seasonDate.periodEnd;
            }
            try
            {
                context.SaveChanges();
            }
            catch (SqlException sqlex)
            {
                throw new Exception("SeasonDate is not saved", sqlex);
            }
        }

        public void AddSeasonDate(SeasonDate seasonDate)
        {
            context.SeasonDates.Add(seasonDate);
            try
            {
                context.SaveChanges();
            }
            catch (SqlException sqlex)
            {
                throw new Exception("seasonDate is not saved", sqlex);
            }
        }
    }
}
