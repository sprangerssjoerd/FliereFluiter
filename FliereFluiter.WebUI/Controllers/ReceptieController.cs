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
        private IPlaceReservationRepository _placeReservationRepository;
        private IGuestRepository _guestRepository;
        public ReceptieController(IPlaceReservationRepository placeReservationRepository, IGuestRepository guestRepository)
        {
            _placeReservationRepository = placeReservationRepository;
            _guestRepository = guestRepository;
        }

        [HttpGet]
        public ActionResult ReceptieStart()
        {
            if ((int)(Session["RoleLvl"]) >= 200)
            {
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
            else
            {
                return View("~/Views/Login/NotLoggedin.cshtml");
            }
        }

        [HttpGet]
        public ActionResult confirmReservation(int PlaceReservationId)
        {
            if ((int)(Session["RoleLvl"]) >= 200)
            {
                var placeReservation = _placeReservationRepository.getPlaceReservationById(PlaceReservationId);
                placeReservation.Guest = _guestRepository.getGuestByGuestId(placeReservation.GuestId);
                ReceptieViewModel model = new ReceptieViewModel
                {
                    placeReservation = placeReservation
                };
                return View(model);
            }
            else
            {
                return View("~/Views/Login/NotLoggedin.cshtml");
            }
        }

    }
}