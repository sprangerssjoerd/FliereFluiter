using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FliereFluiter.Domain.Abstract;
using FliereFluiter.Domain.Entities;
using FliereFluiter.WebUI.Models;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace FliereFluiter.WebUI.Controllers
{
    public class MainController : Controller
    {
        private IGuestRepository _guestRepository;
        private ICampingFieldRepository _fieldRepository;
        private ICampingPlaceRepository _placeRepository;
        public int PageSize = 4;

        public MainController(IGuestRepository guestRepository, ICampingFieldRepository fieldRepository, ICampingPlaceRepository placeRepository)
        {
            this._guestRepository = guestRepository;
            this._fieldRepository = fieldRepository;
            this._placeRepository = placeRepository;
        }


        [HttpGet]
        public ViewResult Index(int page = 1)
        {
            var campingFieldsList = _fieldRepository.CampingFields.ToList();

            MainViewModel model = new MainViewModel
            {
                Guests = _guestRepository.Guests
                    .OrderBy(g => g.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _guestRepository.Guests.Count()
                },

                CampingFields = campingFieldsList,
                CampingPlaces = _placeRepository.CampingPlaces,
            };
            return View(model);
        }
    }
}