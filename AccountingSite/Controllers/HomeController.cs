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
            ViewBag.Message = $"Ваш логин = {User.Identity.Name}, Ваша роль = {User.Identity.AuthenticationType}";
            return View();
        }
    }
}