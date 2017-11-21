using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FliereFluiter.Domain.Abstract;
using FliereFluiter.Domain.Entities;
using FliereFluiter.Domain.Concrete;
using FliereFluiter.WebUI.Models;
using System.Data;
using System.Data.SqlClient;
using System.Xml;


namespace FliereFluiter.WebUI.Controllers
{
    public class GuestController : Controller
    {

        private IGuestRepository _guestRepository;
        private ICampingFieldRepository _fieldRepository;
        private ICampingPlaceRepository _placeRepository;
        private IPlaceReservationRepository _placeReservationRepository;
        private IPlaceReservationCampingPlaceRepository _placeReservationCampingPlaceRepository;

        public GuestController(IGuestRepository guestRepository, ICampingFieldRepository fieldRepository, ICampingPlaceRepository placeRepository, IPlaceReservationRepository placeReservationRepository, IPlaceReservationCampingPlaceRepository placeReservationCampingPlaceRepository)
        {
            this._guestRepository = guestRepository;
            this._fieldRepository = fieldRepository;
            _placeRepository = placeRepository;
            _placeReservationRepository = placeReservationRepository;
            _placeReservationCampingPlaceRepository = placeReservationCampingPlaceRepository;
        }

        [HttpGet]
        public ViewResult Reservation()
        {
            ReservationViewModel model = new ReservationViewModel
            {

            };
            return View(model);
        }

        [HttpPost]
        public ActionResult CheckDate(DateTime beginDate, DateTime endDate, int fieldId)
        {
            var campingField = _fieldRepository.getCampingFieldById(fieldId);

            var placeavailability = _fieldRepository.isDateInBetween(beginDate, endDate, fieldId);

            ArrayList placeIdAv = new ArrayList();
            ArrayList placeNameAv = new ArrayList();
            ArrayList placeBoolAv = new ArrayList();

            for (int i= 0; i < placeavailability.Count; i++)
            {
                PlaceAv placeAv = (PlaceAv)placeavailability[i];
                placeIdAv.Insert(i, placeAv.PlaceId);
                placeNameAv.Insert(i, placeAv.Name);
                placeBoolAv.Insert(i, placeAv.IsTaken);


            }

            CheckDateViewModel model = new CheckDateViewModel
            {
                campingField = campingField,
                placeavailability = placeavailability,
                placeIdAv = placeIdAv,
                placeNameAv = placeNameAv,
                placeBoolAv = placeBoolAv,
                beginDate = beginDate,
                endDate = endDate
            };
            return View(model);

        }

        [HttpPost]
        public ActionResult ReservationGuestDetails(DateTime beginDate, DateTime endDate, int placeId)
        {
            CampingPlace campingPlace = _placeRepository.getCampingPlaceById(placeId);
            ReservationViewModel model = new ReservationViewModel
            {
                beginDate = beginDate,
                endDate = endDate,
                campingPlace = campingPlace

            };
            return View(model);
        }

        [HttpPost]
        public ActionResult addNewGuest(Guest guest, DateTime birthday)
        {
            guest.Birthday = birthday;
            var newGuest = _guestRepository.addGuest(guest);

            addNewGuestViewModel model = new addNewGuestViewModel
            {
                guest = newGuest
            };
            return View(model);
        }

       [HttpPost]
       public ActionResult addnewGuestReservation(Guest guest, CampingPlace campingPlace, DateTime beginDate, DateTime endDate)
        {
            PlaceReservation placeRes = new PlaceReservation();
            PlaceReservationCampingPlace prcp = new PlaceReservationCampingPlace();
            placeRes.GuestId = guest.Id;
            placeRes.GuestAmount = 0;
            placeRes.ChildrenAmount = 0;
            placeRes.Discount = false;

            var newGuest = _guestRepository.addGuest(guest);
            var newPlaceReservation = _placeReservationRepository.addPlaceReservation(placeRes);

            prcp.PlaceReservationId = newPlaceReservation.PlaceReservationId;
            prcp.CampingPlaceId = campingPlace.CampingPlaceId;
            prcp.PeriodBegin = beginDate;
            prcp.PeriodEnd = endDate;

            var newPlaceReservationCampingPlace = _placeReservationCampingPlaceRepository.addPRCP(prcp);

            SuccesFullReservationViewModel model = new SuccesFullReservationViewModel
            {
                guest = newGuest,
                placeReservation = newPlaceReservation,
                placeReservationCampingPlace = newPlaceReservationCampingPlace
            };
            return View();
        }


    }
}