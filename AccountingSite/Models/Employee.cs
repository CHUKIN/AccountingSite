using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AccountingSite.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }
       

        public virtual int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public ICollection<Order> Orders { get; set; }

        public Employee()
        {
            Orders = new List<Order>();
        }
    }
}