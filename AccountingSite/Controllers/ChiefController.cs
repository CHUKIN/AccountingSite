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
            var order = new Order()
            {
                From = db.Employees.FirstOrDefault(i => i.Login == User.Identity.Name),
                To = db.Employees.FirstOrDefault(i => i.Name == db.Employees.FirstOrDefault(j => j.Name == to).Name),
                Text = text,
                Date = DateTime.Now,
                Status = db.Statuses.FirstOrDefault(i => i.Name == "SendToStore")
            };

            db.Orders.Add(order);

            for (var i=0;i<items.Length;i++)
            {
                var item = new ItemTransaction()
                {
                    Name = items[i],
                    Count = int.Parse(counts[i]),
                    Order = order
                };
                db.ItemTransactions.Add(item);
            }


            db.SaveChanges();
            ViewBag.Message = "Успешно добавлено!";
            return View(db);
        }

        public ActionResult ItemExtradition()
        {
            return View(db);
        }

        
    }
}