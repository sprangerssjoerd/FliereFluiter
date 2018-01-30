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

        public IEnumerable<Season> getSeasons()
        {
            try
            {
                IEnumerable<Season> seasons = context.Seasons;
                return seasons;
            }
            catch (NullReferenceException e)
            {
                return null;
            }
        }

        public void UpdateSeason(Season season)
        {
            Season result = context.Seasons.Single(x => x.SeasonID.Equals(season.SeasonID));
            if(result != null)
            {
                result.Name = season.Name;
                result.Price = season.Price;
            }
            try
            {
                context.SaveChanges();
            }
            catch (SqlException sqlex)
            {
                throw new Exception("season is not saved", sqlex);
            }
        }
    }
}
