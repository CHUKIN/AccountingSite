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

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize(Roles ="Employee")]
        public ActionResult Contact()
        {
            db.Statuses.Add(new Status { Name = "123" });
            db.SaveChanges();

            return Json(db.Statuses,JsonRequestBehavior.AllowGet);
        }
    }
}