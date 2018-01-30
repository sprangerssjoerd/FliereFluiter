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
    public class EFFacilityRepository : IFacilityRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Facility> Facilities
        {
            get { return context.Facilities; }
        }

        public void UpdateFacility(Facility facility)
        {
            Facility result = context.Facilities.Single(x => x.FacilityId.Equals(facility.FacilityId));
            if (result != null)
            {
                result.Name = facility.Name;
                result.Totalamount = facility.Totalamount;
                result.Price = facility.Price;
                result.Description = facility.Description;
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

        public void RemoveFacility(Facility facility)
        {
            Facility itemToRemove = context.Facilities.Single(x => x.FacilityId.Equals(facility.FacilityId));
            if(itemToRemove != null)
            {
                context.Facilities.Remove(itemToRemove);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("data you want to deleted does not exist");
            }
        }

        public void AddNewFacility(Facility facility)
        {
            context.Facilities.Add(facility);
            try
            {
                context.SaveChanges();
            }
            catch(SqlException sqlex)
            {
                throw new Exception("facility is not saved", sqlex);
            }
        }
    }
}
