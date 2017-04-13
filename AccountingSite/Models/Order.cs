using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountingSite.Models
{
    public class Order
    {
        public int Id { get; set; }
         public Employee From { get; set; }
         public Employee To { get; set; }
        public string Text { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }

        public DateTime Date { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employees { get; set; }

        public ICollection<ItemTransaction> ItemTransactions { get; set; }


        public Order()
        {
            ItemTransactions = new List<ItemTransaction>();
        }
    }
}