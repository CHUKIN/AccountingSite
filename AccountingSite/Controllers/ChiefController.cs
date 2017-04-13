using AccountingSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountingSite.Controllers
{
    [Authorize(Roles="Chief")]
    public class ChiefController : Controller
    {
        ManageContext db = new ManageContext();

        // GET: Chief
        public ActionResult OrderStatus()
        {
            return View(db.Orders.Where(i => i.From.Login == User.Identity.Name)
                .Include(i => i.Status)
                .Include(i => i.Employee));
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
                Status = db.Statuses.FirstOrDefault(i => i.Name == "Отправлен на склад"),
                Employee = null
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

        [HttpGet]
        public ActionResult ItemExtradition()
        {
            return View(db);
        }

        [HttpPost]
        public ActionResult ItemExtradition(int Id, string To)
        {
            var order = db.Orders.Find(Id);
            var employee = db.Employees.FirstOrDefault(i=>i.Name==To);
            order.Employee = employee;
            order.Status = db.Statuses.FirstOrDefault(i => i.Name == "Назначено");
            ViewBag.Message = "Успешно добавлено!";
            db.SaveChanges();
            return View(db);
        }
    }
}