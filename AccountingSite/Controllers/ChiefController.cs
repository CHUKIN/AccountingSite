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
            string login = UserIdentity.GetUser(User.Identity.Name).Login;
            //return View(db.Orders.Where(i => i.From.Login ==  login|| i.To.Login == login)
            //    .Include(i => i.Status)
            //    .Include(i => i.Employee));
            return View(db.Orders.Where(i => i.From.Login == login || i.To.Login == login)
               .Include(i => i.Status)
               );
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
                Status = db.Statuses.FirstOrDefault(i => i.Name == "Sent to the warehouse"),
               // Employee = null
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
            Writer.Write(order, User.Identity.Name, Server.MapPath("~/Files/"));
            return View(db);
        }

        [HttpGet]
        public ActionResult ItemExtradition()
        {
            return View(db);
        }

        [HttpPost]
        public ActionResult ItemExtradition(int Id, int To)
        {
            var order = db.Orders.Find(Id);
            var employee = db.Employees.Find(To);
            // order.Employee = employee;
            order.EmployeeId = employee.Id;
            order.From = db.Employees.FirstOrDefault(i => i.Login == User.Identity.Name);
            order.To = employee;
            order.Status = db.Statuses.FirstOrDefault(i => i.Name == "Assigned to");
            order.Date = DateTime.Now;
            ViewBag.Message = "Успешно добавлено!";
            db.SaveChanges();
            Writer.Write(order, User.Identity.Name, Server.MapPath("~/Files/"));
            return View(db);
        }


        [HttpGet]
        public ActionResult SendItem()
        {
            var listItem = new List<ItemTransaction>();
            var orders = db.Orders.Where(i => i.Status == db.Statuses.FirstOrDefault(j => j.Name == "Waiting for appointment")).Include(i=>i.ItemTransactions);
            foreach(var order in orders)
            {
                listItem.AddRange(order.ItemTransactions);
            }
            var result = new SendItem();
            result.ItemTransaction = listItem;
            result.Employee = db.Employees.Where(i => i.Role == db.Roles.FirstOrDefault(j => j.Name == "Engineer")).ToList();
            return View(result);
        }

        [HttpPost]
        public ActionResult SendItem(int[] itemId, int employeeId)
        {
            foreach(var id in itemId)
            {
                var newOrder = new Order()
                {
                    Date = DateTime.Now,
                    From = db.Employees.FirstOrDefault(i => i.Login == User.Identity.Name),
                    To = db.Employees.Find(employeeId),
                    Text = "",
                    Status = db.Statuses.FirstOrDefault(i => i.Name == "Assigned to"),
                    EmployeeId = employeeId
                };
                db.Orders.Add(newOrder);
                db.ItemTransactions.Find(id).Order = newOrder;
                Writer.Write(newOrder, User.Identity.Name, Server.MapPath("~/Files/"));
            }

            db.SaveChanges();
            
            return RedirectToAction("SendItem");
        }
    }
}