using AccountingSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountingSite.Controllers
{
    public class HomeController : Controller
    {
        ManageContext db = new ManageContext();

        [Authorize]
        public ActionResult Index()
        {
            if(User.IsInRole("Admin"))
            {
                return View("~/Views/Home/Admin.cshtml");
            }
            if (User.IsInRole("Engineer"))
            {
                return View("~/Views/Home/Engineer.cshtml");
            }
            if (User.IsInRole("Manager"))
            {
                return View("~/Views/Home/Manager.cshtml");
            }
            if (User.IsInRole("Chief"))
            {
                return View("~/Views/Home/Chief.cshtml");
            }
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}