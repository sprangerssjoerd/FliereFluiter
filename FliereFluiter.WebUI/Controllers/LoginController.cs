using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using FliereFluiter.Domain.Abstract;
using FliereFluiter.Domain.Entities;
using FliereFluiter.Domain.Concrete;
using FliereFluiter.WebUI.Models;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace FliereFluiter.WebUI.Controllers
{
    public class LoginController : Controller
    {

        private ILoginRepository _loginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

		public LoginController()
		{

		}
        [HttpGet]
        public ViewResult Login()
        {
            LoginViewModel model = new LoginViewModel
            {
                UserNamePass = true,
                PasswordPass = true
            };
            return View("Login", model);
        }

        [HttpPost]
        public ActionResult Login(string name, string password)
        {
            var user = _loginRepository.GetUserInformation(name);

            if (user != null)
            {
                if (_loginRepository.ValidateLogin(user, password))
                {
                    Session["UserName"] = user.UserName;
                    Session["UserId"] = user.UserId;
                    Session["UserRole"] = user.RoleId;
                    Session["Rolelvl"] = user.Role.RoleLvl;

                    LoginViewModel model = new LoginViewModel
                    {
                        UserId = (int)(Session["UserId"]),
                        UserName = (string)(Session["UserName"]),
                        UserRole = (string)(Session["RoleId"]),
                        RoleLvl = (int)(Session["Rolelvl"])
                    };
                    return View("LoginSuccesFull", model);
                }
                else
                {
                    LoginViewModel model = new LoginViewModel
                    {
                        PasswordPass = false
                    };
                    return View("Login", model);
                }
            }
            else
            {
                LoginViewModel model = new LoginViewModel
                {
                    UserNamePass = false
                };
                return View("Login", model);
            }
        }

        public ActionResult Logout()
        {
            Session["UserName"] = null;
            Session["UserId"] = null;
            Session["UserRole"] = null;
            Session["RoleLvl"] = null;

            LoginViewModel model = new LoginViewModel
            {

            };
            return View("Logout", model);
        }

        public ActionResult NotLoggedin()
        {
            Session["UserName"] = null;
            Session["UserId"] = null;
            Session["UserRole"] = null;
            Session["RoleLvl"] = null;

            LoginViewModel model = new LoginViewModel
            {

            };
            return View("Logout", model);
        }

        public void checkRoleLvl(int ReqLvl)
        {
            try
            {
                int sessionInfo = (int)(Session["Rolelvl"]);
                if (sessionInfo < ReqLvl)
                {
                    throw new Exception("notloggedin");
                }
            }
            catch (Exception ex)
            {
                RedirectToAction("NotLoggedin", "login");
            }
        }
    }
}

