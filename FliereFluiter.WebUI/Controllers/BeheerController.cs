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
    public class BeheerController : Controller
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
        private IFacilityRepository _facilityRepository;
        private IFacilityReservationRepository _facilityReservartionRepository;
        public BeheerController(IPlaceReservationRepository placeReservationRepository, IGuestRepository guestRepository, LoginController loginController, IInvoiceRepository inVoiceRepository, ISeasonDateRepository seasonDateRepository, IPlaceReservationCampingPlaceRepository placeReservationCampingPlaceRepository, ISeasonRepository seasonRepository, PdfCreator pdfCreator, ICampingPlaceRepository campingPlaceRepository,IFacilityRepository facilityRepository, IFacilityReservationRepository facilityReservartionRepository)
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
            _facilityRepository = facilityRepository;
            _facilityReservartionRepository = facilityReservartionRepository;

        }


        public ActionResult BeheerStart()
        {
            _loginController.checkRoleLvl(500);

            return View("BeheerStart");
        }

        public ActionResult seasonProgramming()
        {
            _loginController.checkRoleLvl(500);

            IEnumerable<SeasonDate> seasonDates = _seasonDateRepository.getSeasonDates();
            IEnumerable<Season> seasons = _seasonRepository.getSeasons();


            List<SeasonInfo> seasonInfoList = new List<SeasonInfo>();
            foreach(var p in seasons)
            {
                SeasonDate seasonDate = _seasonDateRepository.getSeasonDateBySeasonId(p.SeasonID);
                SeasonInfo seasonInfo = new SeasonInfo
                {
                    seasonId = p.SeasonID,
                    seasonDateId = seasonDate.SeasondateID,
                    name = p.Name,
                    beginDate = seasonDate.PeriodBegin,
                    endDate = seasonDate.periodEnd,
                    price = p.Price
                };
                seasonInfoList.Add(seasonInfo);
            }

            BeheerViewModel model = new BeheerViewModel
            {
                seasonInfoList = seasonInfoList
            };

            return View("SeasonProgramming", model);
        }

        [HttpGet]
        public ActionResult ChangeSeason(SeasonInfo seasonInfo)
        {
            _loginController.checkRoleLvl(500);

            BeheerViewModel model = new BeheerViewModel
            {
                seasonInfo = seasonInfo,
                beginDate = seasonInfo.beginDate,
                endDate = seasonInfo.endDate
            };
            return View("ChangeSeason", model);
        }
        
        [HttpPost]
        [ActionName("ChangeSeason")]
        public ActionResult ChangeSeasonPost(SeasonInfo seasonInfo)
        {

            _loginController.checkRoleLvl(500);

            Season season = new Season
            {
                Name = seasonInfo.name,
                Price = seasonInfo.price,
                SeasonID = seasonInfo.seasonId
            };

            SeasonDate seasonDate = new SeasonDate
            {
                SeasonId = seasonInfo.seasonId,
                SeasondateID = seasonInfo.seasonDateId,
                PeriodBegin = seasonInfo.beginDate,
                periodEnd = seasonInfo.endDate
            };

            _seasonRepository.UpdateSeason(season);
            _seasonDateRepository.UpdateSeasonDate(seasonDate);

            BeheerViewModel model = new BeheerViewModel
            {
                seasonInfo = seasonInfo,
                
            };
            return View("ChangeSeason", model);
        }

        [HttpGet]
        public ActionResult AddNewSeason()
        {
            _loginController.checkRoleLvl(500);
            return View("addNewSeason");
        }

        [HttpPost]
        public void AddNewSeasonPost(string name, DateTime beginDate, DateTime endDate, double price)
        {

            _loginController.checkRoleLvl(500);

            Season season = new Season
            {
                Name = name,
                Price = price
            };

            Season result = _seasonRepository.AddSeason(season);

            SeasonDate seasonDate = new SeasonDate
            {
                SeasonId = result.SeasonID,
                PeriodBegin = beginDate,
                periodEnd = endDate
            };

            _seasonDateRepository.AddSeasonDate(seasonDate);
            seasonProgramming();
        }

        public ActionResult FacilityManagement()
        {
            _loginController.checkRoleLvl(500);

            return View("FacilityManagement");
        }

        [HttpGet]
        public ActionResult FacilityView()
        {
            IEnumerable<Facility> facilities = _facilityRepository.Facilities;

            List<Facility> facilityList = new List<Facility>();
            foreach(var p in facilities)
            {
                facilityList.Add(p);
            }

            BeheerViewModel model = new BeheerViewModel
            {
                facilityList = facilityList
            };
            return View("FacilityView", model);
        }

        [HttpGet]
        public ActionResult UpdateFacility(Facility facility)
        {

            BeheerViewModel model = new BeheerViewModel
            {
                facility = facility
            };
            return View("UpdateFacility", model);
        }

        [HttpPost]
        public ActionResult UpdateFacilityPost(Facility facility)
        {
            _facilityRepository.UpdateFacility(facility);
            return RedirectToAction("FacilityView");
        }

        [HttpGet]
        public ActionResult RemoveFacility(Facility facility)
        {
            _facilityRepository.RemoveFacility(facility);
            return RedirectToAction("FacilityView");
        }

        [HttpGet]
        public ActionResult AddNewFacility()
        {
            return View("AddNewFacility");
        }

        [HttpPost]
        public ActionResult AddNewFacilityPost(Facility facility)
        {
            _facilityRepository.AddNewFacility(facility);
            return RedirectToAction("FacilityView");
        }

    }
}