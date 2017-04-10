using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingSite.Models
{
    public class ItemTransaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}