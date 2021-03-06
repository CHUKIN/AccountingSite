﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingSite.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }

        public Status()
        {
            Orders = new List<Order>();
        }
    }
}