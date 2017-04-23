using AccountingSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AccountingSite.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                Employee Employee = null;
                using (ManageContext db = new ManageContext())
                {
                    Employee = db.Employees.FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);
                }
                if (Employee != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message= "Пользователя с таким логином и паролем нет";
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Admin")]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Employee Employee = null;
                using (ManageContext db = new ManageContext())
                {
                    Employee = db.Employees.FirstOrDefault(u => u.Login == model.Login);
                }
                if (Employee == null)
                {
                    // создаем нового пользователя
                    using (ManageContext db = new ManageContext())
                    {
                        db.Employees.Add(new Employee { Login = model.Login, Password = model.Password, Role = db.Roles.Where(i => i.Name == "Admin").FirstOrDefault() });
                        db.SaveChanges();

                        Employee = db.Employees.Where(u => u.Login == model.Login && u.Password == model.Password).FirstOrDefault();
                    }
                    // если пользователь удачно добавлен в бд
                    if (Employee != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}