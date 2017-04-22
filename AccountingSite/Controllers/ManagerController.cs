using AccountingSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AccountingSite.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        ManageContext db = new ManageContext();
        // GET: Manager
        [HttpGet]
        public ActionResult ListReturnItem()
        {
            return View(db.Orders.Where(i=>i.To.Login==User.Identity.Name&&(i.Status.Name== "Отсутствие потребности"||i.Status.Name== "Брак отправка")).Include(j=>j.Status).Include(j => j.From).Include(j => j.To).Include(j => j.ItemTransactions));
        }

        [HttpGet]
        public ActionResult SendItem()
        {
            return View(db.Orders.Where(i=>i.To.Login==User.Identity.Name&&i.Status.Name== "Отправлен на склад").Include(i=>i.ItemTransactions).Include(i => i.From).Include(i => i.Status).Include(i => i.To));
        }

        [HttpPost]
        public ActionResult SendItem(int id,bool permission)
        {
            var order = db.Orders.Where(i=>i.Id==id).Include(i => i.ItemTransactions).FirstOrDefault();
            if (permission)
            {

                foreach(var item in order.ItemTransactions)
                {
                    db.Items.FirstOrDefault(i => i.Name == item.Name).Count -= item.Count;
                    if (db.Items.FirstOrDefault(i => i.Name == item.Name).Count <0)
                    {
                        ViewBag.Message = "Нет данного количества";
                        return View(db.Orders.Where(i => i.To.Login == User.Identity.Name && i.Status.Name == "Отправлен на склад").Include(i => i.ItemTransactions).Include(i => i.From).Include(i => i.Status).Include(i => i.To));
                    }
                }
                order.Status = db.Statuses.FirstOrDefault(i => i.Name == "Ожидает назначения");

            }
            else
            {
                order.Status = db.Statuses.FirstOrDefault(i => i.Name == "Отказ");
              
                
            }

            order.To = db.Employees.Find(order.FromId);
            order.From = db.Employees.FirstOrDefault(i => i.Login == User.Identity.Name);
            db.SaveChanges();
            return View(db.Orders.Where(i => i.To.Login == User.Identity.Name && i.Status.Name == "Отправлен на склад").Include(i => i.ItemTransactions).Include(i => i.From).Include(i => i.Status).Include(i => i.To));
        }

        [HttpPost]
        public ActionResult DeleteItem(int id)
        {
            Order order = db.Orders.FirstOrDefault(i=>i.Id==id);
            order.Status = db.Statuses.FirstOrDefault(i=>i.Name=="Утилизирован");
            db.SaveChanges();
            return RedirectToRoute(new { controller = "Manager", action = "ListReturnItem" });
        }

        [HttpPost]   
        public ActionResult RestoreItem(int id)
        {
            var order = db.Orders.Where(i => i.Id == id).Include(i => i.ItemTransactions).FirstOrDefault();
            order.Status = db.Statuses.FirstOrDefault(i => i.Name == "Возвращён");

            foreach(var item in order.ItemTransactions)
            {
                db.Items.FirstOrDefault(i=>i.Name==item.Name).Count+=item.Count;

            }
            db.SaveChanges();
            return RedirectToRoute(new { controller = "Manager", action = "ListReturnItem" });
        }
    }
}