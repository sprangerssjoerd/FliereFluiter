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
    public class ManagerController : Controller
    {

        private LoginController _loginController;
        private PdfCreator _pdfCreator;
        private IPlaceReservationRepository _placeReservationRepository;
        private IGuestRepository _guestRepository;
        private IInvoiceRepository _invoiceRepository;
        private ISeasonDateRepository _seasonDateRepository;
        private IPlaceReservationCampingPlaceRepository _placeReservationCampingPlaceRepository;
        private ISeasonRepository _seasonRepository;
        private ICampingPlaceRepository _campingPlaceRepository;


        public ManagerController(IPlaceReservationRepository placeReservationRepository, IGuestRepository guestRepository, LoginController loginController, IInvoiceRepository inVoiceRepository, ISeasonDateRepository seasonDateRepository, IPlaceReservationCampingPlaceRepository placeReservationCampingPlaceRepository, ISeasonRepository seasonRepository, PdfCreator pdfCreator, ICampingPlaceRepository campingPlaceRepository)
        {
            _placeReservationRepository = placeReservationRepository;
            _guestRepository = guestRepository;
            _loginController = loginController;
            _invoiceRepository = inVoiceRepository;
            _seasonDateRepository = seasonDateRepository;
            _placeReservationCampingPlaceRepository = placeReservationCampingPlaceRepository;
            _seasonRepository = seasonRepository;
            _pdfCreator = pdfCreator;
            _campingPlaceRepository = campingPlaceRepository;
        }

        public ActionResult ManagerStart()
        {
            _loginController.checkRoleLvl(900);

            List<string> nameList = new List<string>();
            List<int> idList = new List<int>();
            foreach(var p in _campingPlaceRepository.CampingPlaces)
            {
                nameList.Add(p.Name);
                idList.Add(p.CampingPlaceId);
            }

            ManagerViewModel model = new ManagerViewModel
            {
                campingnameList = nameList,
                idList = idList
            };

            return View("ManagerStart", model);
        }

        [HttpGet]
        public ActionResult viewOccupancyRate(int id)
        {
            _loginController.checkRoleLvl(900);

            IEnumerable<PlaceReservationCampingPlace> PRCPList = _placeReservationCampingPlaceRepository.getPRCPByCampingPlaceId(id);
            List<string> resultList = new List<string>();
            if (PRCPList.Any())
            {
                PlaceReservationCampingPlace first = PRCPList.First();
                PlaceReservationCampingPlace last = PRCPList.Last();

                DateTime firstInstance = first.PeriodBegin;
                DateTime lastInstance = last.PeriodEnd;

                int firstYear = firstInstance.Year;
                int lastYear = lastInstance.Year;

                int totalYears = lastYear - firstYear;

 
                for (int i = 0; i <= totalYears; i++)
                {
                    double days = 0;
                    int daysinyear = 365;
                    if (DateTime.IsLeapYear(firstYear + i))
                    {
                        daysinyear = 366;
                    }
                    foreach (var p in PRCPList.Where(n => n.PeriodBegin.Year.Equals(firstYear + i)))
                    {
                        if (p.PeriodEnd.Year.Equals(firstYear + i))
                        {
                            days += (p.PeriodEnd - p.PeriodBegin).TotalDays;
                        }
                        else
                        {
                            days += daysinyear - p.PeriodBegin.DayOfYear;
                        }
                    }
                    double rate = (days / daysinyear) * 100;
                    string result = "campingplaats: " + " heeft in het jaar " + (firstYear + i) + " een bezettinggraad van " + rate + "%";
                    resultList.Add(result);
                }
            }
            else
            {
                string result = "deze campingplaats heeft nog geen reserveringen";
                resultList.Add(result);
            }

            ManagerViewModel model = new ManagerViewModel
            {
                occupancyRate = resultList
            };

            return View("BezettinggraadView",model);
        }

        [HttpGet]
        public ActionResult viewGuestList()
        {
            _loginController.checkRoleLvl(900);

            IEnumerable<Guest> guestList = _guestRepository.getAllGuests();
            ManagerViewModel model = new ManagerViewModel
            {
                guestList = guestList
            };
            return View("guestList", model);
        }

        [HttpGet]
        public ActionResult viewAllGuestReservations(int guestId)
        {
            _loginController.checkRoleLvl(900);

            IEnumerable<PlaceReservation> PRList = _placeReservationRepository.getPlaceReservationByGuestId(guestId);
            List<PlaceReservationCampingPlace> PRCPList = new List<PlaceReservationCampingPlace>();
            List<string> stringResult = new List<string>();
            foreach (var p in PRList)
            {
                IEnumerable<PlaceReservationCampingPlace> c = _placeReservationCampingPlaceRepository.getPRCPByPlaceReservationId(p.PlaceReservationId);
                foreach (var PRCP in c)
                {
                    string stringsentence = "gastid: " + p.GuestId + " heeft een gereserveerd op: " + PRCP.PeriodBegin + " tot en met " + PRCP.PeriodEnd + " op campingplaats " + PRCP.CampingPlaceId;
                    stringResult.Add(stringsentence);
                    PRCPList.Add(PRCP);
                }
            }

            ManagerViewModel model = new ManagerViewModel
            {
                bookingStrings = stringResult
            };
            return View("guestBookings", model);
        }

    }
}