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
        public BeheerController(IPlaceReservationRepository placeReservationRepository, IGuestRepository guestRepository, LoginController loginController, IInvoiceRepository inVoiceRepository, ISeasonDateRepository seasonDateRepository, IPlaceReservationCampingPlaceRepository placeReservationCampingPlaceRepository, ISeasonRepository seasonRepository, PdfCreator pdfCreator, ICampingPlaceRepository campingPlaceRepository)
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
            BeheerViewModel model = new BeheerViewModel
            {
                seasonInfo = seasonInfo,
            };
            return View("ChangeSeason", model);
        }
        
        [HttpPost]
        [ActionName("ChangeSeason")]
        public ActionResult ChangeSeasonPost(SeasonInfo seasonInfo)
        {
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
                seasonInfo = seasonInfo
            };
            return View("ChangeSeason", model);
        }

        public ActionResult facilityManagement()
        {
            _loginController.checkRoleLvl(500);

            return View("FacilityManagement");
        }

    }
}