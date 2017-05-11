using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AccountingSite.Models;

namespace AccountingSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeesController : Controller
    {
        private ManageContext db = new ManageContext();

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Department).Include(e => e.Role);
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name");
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Login,Password,Name,Age,RoleId,DepartmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                Employee existEmployee = db.Employees.FirstOrDefault(i=>i.Login==employee.Login);
                if (existEmployee == null)
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
                
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", employee.Id);
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name", employee.RoleId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", employee.DepartmentId);
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name", employee.RoleId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Login,Password,Name,Age,RoleId,DepartmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                Employee existEmployee = db.Employees.Find(employee.Id);
                Employee checkForLogin = db.Employees.FirstOrDefault(i => i.Login == employee.Login);
                if (employee.Login!=existEmployee.Login&&checkForLogin!=null)
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
                else
                {


                    //db.Entry(employee).State = EntityState.Modified;
                    existEmployee.Age = employee.Age;
                existEmployee.Login = employee.Login;
                existEmployee.Password = employee.Password;
                existEmployee.Name = employee.Name;
                existEmployee.RoleId = employee.RoleId;
                existEmployee.DepartmentId = employee.DepartmentId;
                db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", employee.DepartmentId);
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "Name", employee.RoleId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Where(i => i.Id == id).Include(i => i.Role).Include(i=>i.Department).FirstOrDefault();
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee.Role.Name == "Admin"&&db.Employees.Where(i=>i.Role.Name=="Admin").Count()==1)
            {
                ModelState.AddModelError("", "Нельзя удалить единственного администратора");
               
            }
            else
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
