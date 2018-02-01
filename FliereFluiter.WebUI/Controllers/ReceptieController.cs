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
    public class ReceptieController : Controller
    {
        // GET: Receptie
        private LoginController _loginController;
        private PdfCreator _pdfCreator;
        private IPlaceReservationRepository _placeReservationRepository;
        private IGuestRepository _guestRepository;
        private IInvoiceRepository _invoiceRepository;
        private ISeasonDateRepository _seasonDateRepository;
        private IPlaceReservationCampingPlaceRepository _placeReservationCampingPlaceRepository;
        private ISeasonRepository _seasonRepository;
        private ICampingPlaceRepository _campingPlaceRepository;
        private ICampingFieldRepository _campingFieldRepository;
        public ReceptieController(ICampingFieldRepository campingFieldRepositoy, IPlaceReservationRepository placeReservationRepository, IGuestRepository guestRepository, LoginController loginController, IInvoiceRepository inVoiceRepository, ISeasonDateRepository seasonDateRepository, IPlaceReservationCampingPlaceRepository placeReservationCampingPlaceRepository, ISeasonRepository seasonRepository, PdfCreator pdfCreator, ICampingPlaceRepository campingPlaceRepository)
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
            _campingFieldRepository = campingFieldRepositoy;
        }

        [HttpGet]
        public ActionResult ReceptieStart()
        {
            _loginController.checkRoleLvl(200);

            var reservations = _placeReservationRepository.getPlaceReservationsWhereDefIsFalse();
            foreach (var p in reservations)
            {
                p.Guest = _guestRepository.getGuestByGuestId(p.GuestId);
            }

            ReceptieViewModel model = new ReceptieViewModel
            {
                placeReservations = reservations
            };
            return View(model);
        }

        [HttpGet]
        public string confirmReservation(int PlaceReservationId)
        {
            _loginController.checkRoleLvl(200);

            var placeReservation = _placeReservationRepository.getPlaceReservationById(PlaceReservationId);
            placeReservation.Guest = _guestRepository.getGuestByGuestId(placeReservation.GuestId);
            _placeReservationRepository.setDefRes(placeReservation);

            string text = "succelvol reservering geaccepteerd";
            return text;
        }

        public ViewResult viewReservations()
        {
            _loginController.checkRoleLvl(200);

            var reservations = _placeReservationRepository.PlaceReservations;
            foreach(var res in reservations)
            {
                res.Guest = _guestRepository.getGuestByGuestId(res.GuestId);
                res.PlaceReservationCampingPlaces = _placeReservationCampingPlaceRepository.getPRCPByPlaceReservationId(res.PlaceReservationId);
                foreach (var place in res.PlaceReservationCampingPlaces)
                {
                    place.CampingPlace = _campingPlaceRepository.getCampingPlaceById(place.CampingPlaceId);
                }

            }

            ReceptieViewModel model = new ReceptieViewModel
            {
                placeReservations = reservations
            };
            return View("ReservationView",model);
        }

        public int getInvoiceByReservationId(int ResId)
        {
            _loginController.checkRoleLvl(200);

            Invoice invoice = _invoiceRepository.getInvoicesByPlaceReservationId(ResId);
            int invoicePlaceResId = new int();
            if (invoice == null)
            {
                invoicePlaceResId = 0;
            }
            else
            {
                invoicePlaceResId = invoice.PlaceReservationId;
            }
            return invoicePlaceResId;
        }

        public ViewResult MakeInvoice(int ResId)
        {
            _loginController.checkRoleLvl(200);

            IEnumerable<PlaceReservationCampingPlace> placeCamRes = _placeReservationCampingPlaceRepository.getReservationsPlaceByCampingPlaceId(ResId);
            string factuurText = "Factuur" + ResId.ToString() + "\n";

            foreach (var p in placeCamRes)
            {
                IEnumerable<SeasonDate> ESeason = _seasonDateRepository.getRelevantSeasonDates(p.PeriodBegin, p.PeriodEnd);

                CampingPlace C = _campingPlaceRepository.getCampingPlaceById(p.CampingPlaceId);

                foreach (var s in ESeason)
                {
                    var season = _seasonRepository.getSeasonById(s.SeasonId);
                    s.Season = season;
                    double bedrag = CalculatePriceForSeason(s.PeriodBegin, s.periodEnd, p.PeriodBegin, p.PeriodEnd, s.Season.Price);
                    factuurText = factuurText + "Voor Camping plaats \n" + C.Name + "\n in de periode van " + p.PeriodBegin.ToString() + " tot " + p.PeriodEnd.ToString() + "wat in het " + s.Season.Name + " seizoen valt.\n voor een bedrag van: " + bedrag.ToString(); ;

                }
            }
            _pdfCreator.AddLineToPDF(factuurText, ResId);

            ReceptieViewModel model = new ReceptieViewModel
            {

            };

            return View("MakeInvoice");
        }

        private double CalculatePriceForSeason(DateTime seasonBegin, DateTime seasonEnd, DateTime begin, DateTime end, double seasonPrice)
        {
            double bedrag;
            //period is inside season
            if (seasonBegin.Ticks < begin.Ticks && seasonEnd.Ticks > end.Ticks)
            {
                double days = (end - begin).TotalDays;
                bedrag = days * seasonPrice;
                return bedrag;
            }
            //period is outside season
            else if (seasonBegin.Ticks > begin.Ticks && seasonEnd.Ticks < end.Ticks)
            {
                double days = (seasonEnd - seasonBegin).TotalDays;
                bedrag = days * seasonPrice;
                return bedrag;
            }
            //period begin is inside season but end is not
            else if (seasonBegin.Ticks < begin.Ticks && seasonEnd.Ticks < end.Ticks)
            {
                double days = (seasonEnd - begin).TotalDays;
                bedrag = days * seasonPrice;
                return bedrag;
            }
            //period end is inside season but begin is not
            else if(seasonBegin.Ticks > begin.Ticks && seasonEnd.Ticks > end.Ticks)
            {
                double days = (end - seasonBegin).TotalDays;
                bedrag = days * seasonPrice;
                return bedrag;
            }
            return bedrag = 0;
        }

        public void setDiscount(int id)
        {
            _loginController.checkRoleLvl(200);

            _placeReservationRepository.setDiscount(id);
            
        }

        public ActionResult RemoveReservation(int PlaceReservationId)
        {
            _placeReservationRepository.RemovePR(PlaceReservationId);

            return RedirectToAction("viewReservations");
        }
        //to test tomorrow  difficult
        public ViewResult UpdateReservation(int PlaceReservationId)
        {
            PlaceReservation pr = _placeReservationRepository.getPlaceReservationById(PlaceReservationId);

            pr.PlaceReservationCampingPlaces = _placeReservationCampingPlaceRepository.getPRCPByPlaceReservationId(pr.PlaceReservationId);
            IEnumerable<CampingPlace> campingPlaces = _campingPlaceRepository.CampingPlaces;
            List<CampingPlace> campingPlaceList = new List<CampingPlace>();

            foreach (var campingplace in campingPlaces)
            {
                campingPlaceList.Add(campingplace);
            }

            List<PlaceReservationCampingPlace> prcpList = pr.PlaceReservationCampingPlaces.ToList();

            ReceptieViewModel model = new ReceptieViewModel
            {
                prcpList = prcpList,
                campingPlaceList = campingPlaceList
            };
            return View("UpdateReservation", model);
        }

        public ActionResult UpdateReservationPost(List<PlaceReservationCampingPlace> prcpList)
        {
            foreach (var prcp in prcpList)
            {
                DateTime begin = prcp.PeriodBegin;
                DateTime end = prcp.PeriodEnd;
                //PlaceReservationCampingPlace prcp = new PlaceReservationCampingPlace()
                //{
                //    CampingPlaceId = CampingPlaceId,
                //    PlaceReservationId = PlaceReservationId,
                //    PeriodBegin = PeriodBegin,
                //    PeriodEnd = PeriodEnd
                //};
                if (_campingFieldRepository.isDateAfterBegindate(begin, end) != true)
                {
                    throw new Exception("einddatum is voor begindatum");
                }

                var reservations = _placeReservationCampingPlaceRepository.getPRCPByCampingPlaceId(prcp.CampingPlaceId);
                List<PlaceReservationCampingPlace> result = new List<PlaceReservationCampingPlace>();
                foreach (var p in reservations)
                {
                    if (p.PlaceReservationId != prcp.PlaceReservationId)
                    {
                        result.Add(p);
                    }

                }
                bool IsTaken = false;
                foreach (var r in result)
                {

                    //checking if the begin date is between an already existing reservation
                    if ((begin.Ticks > r.PeriodBegin.Ticks && end.Ticks < r.PeriodEnd.Ticks)
                        || (end.Ticks > r.PeriodBegin.Ticks && end.Ticks < r.PeriodEnd.Ticks)
                        || (begin.Ticks < r.PeriodBegin.Ticks && begin.Ticks > r.PeriodEnd.Ticks))
                    {
                        IsTaken = true;
                    }
                }

                if (IsTaken)
                {
                    throw new Exception("Plaats is bezet is gekozen tijds periode");
                }
                else
                {
                    _placeReservationCampingPlaceRepository.UpdatePRCP(prcp);
                }
            }

            return RedirectToAction("viewReservations");
        }
    }
}