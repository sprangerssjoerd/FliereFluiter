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
        private IPlaceReservationRepository _placeReservationRepository;
        private IGuestRepository _guestRepository;
        private IInvoiceRepository _invoiceRepository;
        public ReceptieController(IPlaceReservationRepository placeReservationRepository, IGuestRepository guestRepository, LoginController loginController, IInvoiceRepository inVoiceRepository)
        {
            _placeReservationRepository = placeReservationRepository;
            _guestRepository = guestRepository;
            _loginController = loginController;
            _invoiceRepository = inVoiceRepository;
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

            ReceptieViewModel model = new ReceptieViewModel
            {
                placeReservations = reservations
            };
            return View("ReservationView",model);
        }

        public int getInvoiceByReservationId(int ResId)
        {
            Invoice invoice = _invoiceRepository.getInvoicesByPlaceReservationId(ResId);
            int invoicePlaceResId = invoice.PlaceReservationId;
            return invoicePlaceResId;
        }

        public ViewResult MakeInvoice()
        {

            return View("");
        }
    }
}