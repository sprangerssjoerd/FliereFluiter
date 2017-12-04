using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Moq;
using FliereFluiter.Domain.Entities;
using FliereFluiter.Domain.Concrete;
using FliereFluiter.Domain.Abstract;

namespace FliereFluiter.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            //put bindings here

            kernel.Bind<IGuestRepository>().To<EFGuestRepository>();
            kernel.Bind<ICampingFieldRepository>().To<EFCampingFieldRepository>();
            kernel.Bind<ICampingPlaceRepository>().To<EFCampingPlaceRepository>();
            kernel.Bind<IPlaceReservationRepository>().To<EFPlaceReservationRepository>();
            kernel.Bind<IFacilityRepository>().To<EFFacilityRepository>();
            kernel.Bind<IInvoiceRepository>().To<EFInvoiceRepository>();
            kernel.Bind<IFellowGuestRepository>().To<EFFellowGuestRepository>();
            kernel.Bind<IFacilityReservationRepository>().To<EFFacilityReservationRepository>();
            kernel.Bind<IInvoiceSeasonRepository>().To<EFInvoiceSeasonRepository>();
            kernel.Bind<IPlaceReservationCampingPlaceRepository>().To<EFPlaceReservationCampingPlaceRepository>();
            kernel.Bind<IPlaceReservationFellowGuestRepository>().To<EFPlaceReservationFellowGuestRepository>();
            kernel.Bind<ISeasonDateRepository>().To<EFSeasonDateRepository>();
            kernel.Bind<ISeasonRepository>().To<EFSeasonRepository>();
            kernel.Bind<ILoginRepository>().To<EFLoginRepository>();

            //First mock to test bindings
            //Mock<IGuestRepository> mock = new Mock<IGuestRepository>();
            //mock.Setup(m => m.Guests).Returns(new List<Guest>
            //{
            //    new Guest { Guest_ID = 1, Guest_Name = "Sjoerd Sprangers", Guest_Adress = "Slotlaan 36", Guest_Birthday = new DateTime(1995, 01, 12), Guest_City ="Breda", Guest_Telnumber = 0765615275, Guest_Mobnumber=0637300537, Guest_Postalcode="4851ED", Guest_Socialcard="2544864", Guest_Socialnumber=210101010 },
            //    new Guest { Guest_ID = 2, Guest_Name = "Senne van Gool", Guest_Adress = "Baarschotsestraat 22", Guest_Birthday = new DateTime(2000, 01, 07), Guest_City ="Breda", Guest_Telnumber = 0756451268, Guest_Mobnumber=0637344537, Guest_Postalcode="5851EL", Guest_Socialcard="4744864", Guest_Socialnumber=213101010 },
            //    new Guest { Guest_ID = 3, Guest_Name = "Ivo vador otton", Guest_Adress = "dwerfiets straat 2", Guest_Birthday = new DateTime(1993, 08, 20), Guest_City ="Ettenleur", Guest_Telnumber = 068645712, Guest_Mobnumber=063425537, Guest_Postalcode="5651KD", Guest_Socialcard="7244864", Guest_Socialnumber=100145210 },
            //    new Guest { Guest_ID = 4, Guest_Name = "Brian de Liefde", Guest_Adress = "brabant plein 2", Guest_Birthday = new DateTime(1994, 07, 14), Guest_City ="Breda", Guest_Telnumber = 0684215712, Guest_Mobnumber=0637355537, Guest_Postalcode="4751KD", Guest_Socialcard="8044864", Guest_Socialnumber=210145210 },
            //});

            //kernel.Bind<IGuestRepository>().ToConstant(mock.Object);

        }
    }
}