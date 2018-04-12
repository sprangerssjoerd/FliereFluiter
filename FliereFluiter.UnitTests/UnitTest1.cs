using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FliereFluiter.Domain.Abstract;
using FliereFluiter.Domain.Entities;
using FliereFluiter.WebUI.Controllers;
using System;
using System.Web.Mvc;
using FliereFluiter.WebUI.Models;
using FliereFluiter.WebUI.HtmlHelpers;

namespace FliereFluiter.UnitTests
{
	
	/// <summary>
	/// De unittest lukken niet omdat naar mijn inziens ik niet de mogelijkheid kan krijgen om op de juiste datastructeren, de mocks, de handeling uit te voeren
	/// </summary>
	[TestClass]
	public class UnitTest2
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public UnitTest2()
		{

        }

		public Mock<ISeasonRepository> mockSeasons = new Mock<ISeasonRepository>();
		public Mock<ISeasonDateRepository> mockSeasonDates = new Mock<ISeasonDateRepository>();
		public Mock<IGuestRepository> mockGuests = new Mock<IGuestRepository>();
		public Mock<IPlaceReservationCampingPlaceRepository> mockPRCPs = new Mock<IPlaceReservationCampingPlaceRepository>();
		public Mock<IPlaceReservationRepository> mockPlaceReservations = new Mock<IPlaceReservationRepository>();
		public static Mock<ILoginRepository> mockILoginRepository = new Mock<ILoginRepository>();
		public Mock<LoginController> mockLoginController = new Mock<LoginController>(mockILoginRepository.Object);
		public Mock<IInvoiceRepository> mockInvoices = new Mock<IInvoiceRepository>();
		public Mock<PdfCreator> mockPdfCreator = new Mock<PdfCreator>();
		public Mock<ICampingPlaceRepository> mockCampingPlaces = new Mock<ICampingPlaceRepository>();
		public Mock<IFacilityRepository> mockFacilities = new Mock<IFacilityRepository>();
		public Mock<IFacilityReservationRepository> mockFacilityReservations = new Mock<IFacilityReservationRepository>();
		//object mockContext = new Mock<ISeasonRepository>().Object();

		/// <summary>
		/// setup for all unittests
		/// </summary>
		[TestMethod]
		public void setupTests()
		{
			//var mockPlaceReservations = new Mock<IPlaceReservationRepository>();
			//var sut = new PlaceReservation();

			mockSeasons.Setup(m => m.Seasons).Returns(new Season[]
			{
				new Season { SeasonID = 1, Name = "okt" },
				new Season { SeasonID = 2, Name = "sept" }
			});

			mockSeasonDates.Setup(m => m.SeasonDates).Returns(new SeasonDate[]
			{
				new SeasonDate { SeasondateID = 1, Season = new Season { SeasonID = 1, Name = "okt" } },
				new SeasonDate { SeasondateID = 2, Season = new Season { SeasonID = 1, Name = "okt" } }
			});

			mockGuests.Setup(m => m.Guests).Returns(new Guest[]
			{
				new Guest {
					Adress = "adressstring",
					Birthday = DateTime.Now,
					City = "city string",
					Id = 1,
					Mobnumber = "000000000",
					Name = "nameString",
					Postalcode = "4851ED",
					Socialcard = "6463486161691",
					Socialnumber = "5646168",
					Telnumber = "8794986191",
				}
			});

			mockPlaceReservations.Setup(m => m.PlaceReservations).Returns(new PlaceReservation[]
			{
				new PlaceReservation {
					PlaceReservationId = 1,
					GuestId = 1,
					ChildrenAmount = 0,
					DefReservation = true,
					Discount = false,
					GuestAmount = 1,
					Guest = new Guest {
						#region new guest
						Adress = "adressstring",
						Birthday = DateTime.Now,
						City = "city string",
						Id = 1,
						Mobnumber = "000000000",
						Name = "nameString",
						Postalcode = "4851ED",
						Socialcard = "6463486161691",
						Socialnumber = "5646168",
						Telnumber = "8794986191",
					}
					#endregion
					}

			});
			
		}

		/// <summary>
		/// UnitTest for backlog item 3.3 Data voor seizoenen invoeren
		/// </summary>
		[TestMethod]
		public void Can_AddNewSeason()
		{
			//Arrange - opstellen
			setupTests();
			BeheerController beheerController = new BeheerController(mockPlaceReservations.Object, mockGuests.Object, mockLoginController.Object, mockInvoices.Object, mockSeasonDates.Object,mockPRCPs.Object, mockSeasons.Object, mockPdfCreator.Object, mockCampingPlaces.Object, mockFacilities.Object, mockFacilityReservations.Object);
			
			//Act - uitvoeren
			//beheerController.AddNewSeasonPost("testseason", DateTime.Now, DateTime.Now.AddDays(5), 5);
			var ms = mockSeasons.Object;
			Season result = ms.AddSeason(new Season { Name = "testseason", Price = 500 });

			//Assert - controleren
			//var henk = mockSeasons.Object.Seasons.Single(x => x.SeasonID == 3);
			Assert.AreEqual(result.Name, "testseason");
			//Assert.AreNotEqual(henk.Name, "sept");
		}

		/// <summary>
		/// UnitTest for backlog item 2.6 Verwijderen als receptie
		/// </summary>
		[TestMethod]
		public void Can_RemovePR()
		{
			//arrange
			setupTests();
			//act
			var mp = mockPlaceReservations.Object;
			mp.RemovePR(mp.PlaceReservations.ElementAt(0).PlaceReservationId);


			//assert
			var testresult = mp.PlaceReservations;

			Assert.IsNull(testresult, "The placereservation is not removed");
		}
	}
    //[TestClass]
    //public class UnitTest1
    //{
    //    [TestMethod]
    //    public void Can_Paginate()
    //    {

    //        // Arrange
    //        Mock<IGuestRepository> mock = new Mock<IGuestRepository>();
    //        mock.Setup(m => m.Guests).Returns(new Guest[]
    //        {
    //            new Guest { Id = 1, Name = "G1" },
    //            new Guest { Id = 2, Name = "G2" },
    //            new Guest { Id = 3, Name = "G3" },
    //            new Guest { Id = 4, Name = "G4" },
    //            new Guest { Id = 5, Name = "G5" },
    //        });

    //        //arrange
    //        MainController controller = new MainController(mock.Object);
    //        controller.PageSize = 3;

    //        //act
    //       MainViewModel result = (MainViewModel)controller.List(2).Model;

    //        // Assert
    //        Guest[] prodArray = result.Guests.ToArray();
    //        Assert.IsTrue(prodArray.Length == 2);
    //        Assert.AreEqual(prodArray[0].Name, "G4");
    //        Assert.AreEqual(prodArray[1].Name, "G5");
    //    }

    //    [TestMethod]
    //    public void Can_Generate_Page_Links()
    //    {
    //        //arrange - define an HTML helper - we need to do this in order to apply the extension method
    //        HtmlHelper myHelper = null;

    //        //arrange - create PagingInfo data
    //        PagingInfo pagingInfo = new PagingInfo
    //        {
    //            CurrentPage = 2,
    //            TotalItems = 28,
    //            ItemsPerPage = 10
    //        };

    //        //arrange - set up the delegate using a lambda expression
    //        Func<int, string> pageUrlDelegate = i => "Page" + i;

    //        //act
    //        MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

    //        //assert
    //        Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"+ @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"+ @"<a class=""btn btn-default"" href=""Page3"">3</a>",
    //        result.ToString());
    //    }

    //    [TestMethod]
    //    public void Can_Send_Pagination_View_Model()
    //    {
    //        //arrange
    //        Mock<IGuestRepository> mock = new Mock<IGuestRepository>();
    //        mock.Setup(m => m.Guests).Returns(new Guest[]
    //        {
    //            new Guest { Id = 1, Name = "G1" },
    //            new Guest { Id = 2, Name = "G2" },
    //            new Guest { Id = 3, Name = "G3" },
    //            new Guest { Id = 4, Name = "G4" },
    //            new Guest { Id = 5, Name = "G5" },
    //        });

    //        //arrange
    //        MainController controller = new MainController(mock.Object);
    //        controller.PageSize = 3;

    //        //act
    //        MainViewModel result = (MainViewModel)controller.List(2).Model;

    //        //assert
    //        PagingInfo pageInfo = result.PagingInfo;
    //        Assert.AreEqual(pageInfo.CurrentPage, 2);
    //        Assert.AreEqual(pageInfo.ItemsPerPage, 3);
    //        Assert.AreEqual(pageInfo.TotalItems, 5);
    //        Assert.AreEqual(pageInfo.TotalPages, 2);

    //    }
    //}


}
