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
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {

            // Arrange
            Mock<IGuestRepository> mock = new Mock<IGuestRepository>();
            mock.Setup(m => m.Guests).Returns(new Guest[]
            {
                new Guest { Id = 1, Name = "G1" },
                new Guest { Id = 2, Name = "G2" },
                new Guest { Id = 3, Name = "G3" },
                new Guest { Id = 4, Name = "G4" },
                new Guest { Id = 5, Name = "G5" },
            });

            //arrange
            MainController controller = new MainController(mock.Object);
            controller.PageSize = 3;

            //act
           MainViewModel result = (MainViewModel)controller.List(2).Model;

            // Assert
            Guest[] prodArray = result.Guests.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "G4");
            Assert.AreEqual(prodArray[1].Name, "G5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            //arrange - define an HTML helper - we need to do this in order to apply the extension method
            HtmlHelper myHelper = null;

            //arrange - create PagingInfo data
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            //arrange - set up the delegate using a lambda expression
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            //act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            //assert
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"+ @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"+ @"<a class=""btn btn-default"" href=""Page3"">3</a>",
            result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            //arrange
            Mock<IGuestRepository> mock = new Mock<IGuestRepository>();
            mock.Setup(m => m.Guests).Returns(new Guest[]
            {
                new Guest { Id = 1, Name = "G1" },
                new Guest { Id = 2, Name = "G2" },
                new Guest { Id = 3, Name = "G3" },
                new Guest { Id = 4, Name = "G4" },
                new Guest { Id = 5, Name = "G5" },
            });

            //arrange
            MainController controller = new MainController(mock.Object);
            controller.PageSize = 3;

            //act
            MainViewModel result = (MainViewModel)controller.List(2).Model;

            //assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);

        }
    }


}
