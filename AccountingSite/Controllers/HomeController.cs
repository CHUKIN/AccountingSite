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
                return RedirectToRoute(new { controller = "Employees", action = "Index" });
            }
            if (User.IsInRole("Engineer"))
            {
                return RedirectToRoute(new { controller = "Engineer", action = "ListOrders" });
            }
            if (User.IsInRole("Manager"))
            {
                return RedirectToRoute(new { controller = "Manager", action = "ListReturnItem" });
            }
            if (User.IsInRole("Chief"))
            {
                return RedirectToRoute(new { controller = "Chief", action = "OrderStatus" });
            }
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}