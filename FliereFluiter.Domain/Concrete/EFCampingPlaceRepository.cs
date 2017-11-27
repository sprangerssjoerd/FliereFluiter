using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FliereFluiter.Domain.Abstract;
using FliereFluiter.Domain.Entities;
using System.Collections.Generic;

namespace FliereFluiter.Domain.Concrete
{
    public class EFCampingPlaceRepository : ICampingPlaceRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<CampingPlace> CampingPlaces
        {
            get { return context.CampingPlaces; }
        }

        public IEnumerable<CampingPlace> getCampingPlaceByFieldId(int FieldId)
        {
            return context.CampingPlaces.Where(m => m.CampingFieldId.Equals(FieldId));
        }

        public CampingPlace getCampingPlaceById(int CampingPlaceId)
        {
            try
            {
                var campingPlace = context.CampingPlaces.Single(c => c.CampingPlaceId.Equals(CampingPlaceId));
                return campingPlace;
            }
            catch(Exception ex)
            {
                throw new Exception("ID not Found", ex);
            }
        }
    }
}
