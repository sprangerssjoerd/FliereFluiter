using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FliereFluiter.Domain.Abstract;
using FliereFluiter.Domain.Entities;
using FliereFluiter.Domain.Concrete;
using System.Collections.Generic;
using System.Collections;

namespace FliereFluiter.Domain.Concrete
{
    public class EFCampingFieldRepository : ICampingFieldRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<CampingField> CampingFields
        {
            get { return context.CampingFields; }
        }

        public CampingField getCampingFieldById(int campingFieldId)
        {
            try
            {
                var campfield = context.CampingFields.Single(c => c.CampingFieldId == campingFieldId);
                return campfield;
            }
            catch(Exception ex)
            {
                throw new Exception("ID not Found", ex);
            }
   
        }

        public bool isDateAfterBegindate(DateTime beginDateToCheck, DateTime endDateToCheck)
        {
            bool isDateAfterBeginDate = new bool();
            if (beginDateToCheck.Ticks > endDateToCheck.Ticks)
            {
                return isDateAfterBeginDate = false;
            }
            return isDateAfterBeginDate = true;
        }

        //Function to check wich places on a campingfield are free in a giving period
        public ArrayList isDateInBetween(DateTime beginDateToCheck, DateTime endDateToCheck, int campingfieldId)
        {
            List<string> PlaceAvailability = new List<string>();
            ArrayList pal = new ArrayList();

            EFCampingPlaceRepository campingPlacerep = new EFCampingPlaceRepository();
            EFPlaceReservationCampingPlaceRepository ReservationPlaceRep = new EFPlaceReservationCampingPlaceRepository();

            var f = getCampingFieldById(campingfieldId);
            var places = campingPlacerep.getCampingPlaceByFieldId(f.CampingFieldId);
            
            foreach (var p in places)
            {
                bool IsTaken = new bool();
                IsTaken = false;

                var reservations = ReservationPlaceRep.getReservationsPlaceByCampingPlaceId(p.CampingPlaceId);
                foreach (var r in reservations)
                {
                    //checking if the begin date is between an already existing reservation
                    if ((beginDateToCheck.Ticks > r.PeriodBegin.Ticks && beginDateToCheck.Ticks < r.PeriodEnd.Ticks)
                        || (endDateToCheck.Ticks > r.PeriodBegin.Ticks && endDateToCheck.Ticks < r.PeriodEnd.Ticks)
                        || (beginDateToCheck.Ticks < r.PeriodBegin.Ticks && endDateToCheck.Ticks > r.PeriodEnd.Ticks))
                    {
                        IsTaken = true;
                    }
                }

                PlaceAv placeAv = newPlaceAv(p.CampingPlaceId, p.Name, IsTaken);
                    //= new { placeid = p.CampingPlaceId, placename = p.Name, IsTaken = IsTaken };
                pal.Add(placeAv);
                /*
                //fill the list with the information if an place is empty or not
                // when Istaken is false, the camping place is free in the giving period
                if (IsTaken == false)
                {

                    PlaceAvailability.Add("campingplaats " + p.Name + " is vrij in de aangegeven periode");
                }

                // when Istaken is true, the camping place is not free in the giving period
                else if (IsTaken == true)
                {
                    PlaceAvailability.Add("campingplaats " + p.Name + " is bezet in de aangegeven periode");
                }*/

            }
            return pal;
        }

        public PlaceAv newPlaceAv(int _placeId, string _placeName, bool _isTaken)
        {
            PlaceAv placeAv = new PlaceAv();
            placeAv.PlaceId = _placeId;
            placeAv.Name = _placeName;
            placeAv.IsTaken = _isTaken;
            return placeAv;
        }
    }
}