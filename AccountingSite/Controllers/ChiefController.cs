using AccountingSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountingSite.Controllers
{
    public class ChiefController : Controller
    {
        ManageContext db = new ManageContext();

        // GET: Chief
        public ActionResult OrderStatus()
        {
            return View(db.Orders.Where(i=>i.From.Login==User.Identity.Name));
        }

        public ActionResult StoreRequest()
        {
            return View(db);
        }

        public ActionResult ItemExtradition()
        {
            return View(db);
        }

        
    }
}