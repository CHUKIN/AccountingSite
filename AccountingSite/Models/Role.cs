﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingSite.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public Role()
        {
            Employees = new List<Employee>();
        }
    }
}