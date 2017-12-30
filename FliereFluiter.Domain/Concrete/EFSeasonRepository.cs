using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FliereFluiter.Domain.Abstract;
using FliereFluiter.Domain.Entities;
using System.Collections.Generic;

namespace FliereFluiter.Domain.Concrete
{
    public class EFSeasonRepository : ISeasonRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Season> Seasons
        {
            get { return context.Seasons; }
        }

        public Season getSeasonById(int Id)
        {
            try
            {
                Season season = context.Seasons.Single(m => m.SeasonID.Equals(Id));
                return season;
            }
            catch(NullReferenceException e)
            {
                return null;
            }

        }
    }
}
