using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FliereFluiter.Domain.Abstract;
using FliereFluiter.Domain.Concrete;
using System.Data.Entity;

namespace FliereFluiter.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //Database.SetInitializer<EFDbContext>(new DropCreateDatabaseAlways<EFDbContext>());
            //Database.SetInitializer<EFDbContext>(null);
        }
    }
}
