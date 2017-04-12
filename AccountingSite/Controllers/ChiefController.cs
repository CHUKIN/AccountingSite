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

        [HttpGet]
        public ActionResult StoreRequest()
        {
            return View(db);
        }

        [HttpPost]
        public ActionResult StoreRequest(string[] counts,string[] items, string text, string to)
        {
            Order order = new Order()
            {
                From = db.Employees.Where(i => i.Name == User.Identity.Name).FirstOrDefault(),
                To = db.Employees.Where(i => i.Name == db.Employees.Where(j => j.Name == to).FirstOrDefault().Name).FirstOrDefault(),
                Text = text,
                Date = DateTime.Now,
                Status = db.Statuses.Where(i => i.Name == "SendToStore").FirstOrDefault()
            };

            db.Orders.Add(order);




            db.SaveChanges();
            return Content(order.From.Name);
        }

        public ActionResult ItemExtradition()
        {
            return View(db);
        }

        
    }
}