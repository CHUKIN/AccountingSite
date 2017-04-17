using AccountingSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountingSite.Controllers
{
    public class ManagerController : Controller
    {
        ManageContext db = new ManageContext();
        // GET: Manager
        public ActionResult ListReturnItem()
        {
            return View(db.Orders.Where(i=>i.To.Login==User.Identity.Name&&(i.Status.Name== "Отсутствие потребности"||i.Status.Name== "Брак отправка")).Include(j=>j.Status).Include(j => j.From).Include(j => j.To).Include(j => j.ItemTransactions));
        }
    }
}