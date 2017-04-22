using AccountingSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountingSite.Controllers
{
    [Authorize(Roles = "Engineer")]
    public class EngineerController : Controller
    {
        ManageContext db = new ManageContext();

        // GET: Engineer
        public ActionResult ListOrders()
        {
            return View(db.Orders.Where(i=>i.Employee.Login==User.Identity.Name&&i.Status.Name== "Назначено").Include(i => i.ItemTransactions));
        }

        [HttpGet]
        public ActionResult ReturnOrders()
        {
            return View(db);
        }

        [HttpPost]
        public ActionResult ReturnOrders(int Id, int To, string Reason, string text)
        {
            var order = db.Orders.Find(Id);
            order.Text = text;
            order.From=db.Employees.FirstOrDefault(i=>i.Login==User.Identity.Name);
            order.To = db.Employees.Find(To);
            order.Status = db.Statuses.FirstOrDefault(i => i.Name == Reason);
            order.Date = DateTime.Now;
            order.Employee = null;
            db.SaveChanges();
            ViewBag.Message = "Отправлено";
            return View(db);
        }
    }
}