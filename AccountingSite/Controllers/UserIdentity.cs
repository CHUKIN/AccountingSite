using AccountingSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace AccountingSite.Controllers
{
    public class UserIdentity
    {
        private static ManageContext db = new ManageContext();
        private static Employee instance;

        private UserIdentity()
        { }

        public static Employee GetUser(string name)
        {
            if (instance == null)
                instance = db.Employees.FirstOrDefault(i=>i.Login==name);
            return instance;
        }

       
    }
}