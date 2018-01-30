using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FliereFluiter.Domain.Entities;

namespace FliereFluiter.Domain.Abstract
{
    public interface ISeasonDateRepository
    {
        IEnumerable<SeasonDate> SeasonDates { get; }
        IEnumerable<SeasonDate> getRelevantSeasonDates(DateTime begin, DateTime end);
        IEnumerable<SeasonDate> getSeasonDates();
        SeasonDate getSeasonDateBySeasonId(int seasonId);
        void UpdateSeasonDate(SeasonDate seasonDate);
        void AddSeasonDate(SeasonDate seasonDate);
    }

}
