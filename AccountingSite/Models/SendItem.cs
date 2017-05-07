using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingSite.Models
{
    public class SendItem
    {
        public List<ItemTransaction> ItemTransaction { get; set; }
        public List<Employee> Employee { get; set; }
    }
}