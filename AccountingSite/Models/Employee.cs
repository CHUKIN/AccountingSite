using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingSite.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
       

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public Department Department { get; set; }

        public ICollection<Order> Orders { get; set; }

        public Employee()
        {
            Orders = new List<Order>();
        }
    }
}