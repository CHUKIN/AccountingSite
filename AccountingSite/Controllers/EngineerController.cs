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

        public ActionResult ReturnOrders()
        {
            return View();
        }
    }
}