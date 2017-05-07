using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AccountingSite.Models
{
    public class Order
    {
        public int Id { get; set; }


        public int EmployeeId { get; set; }

        //public Employee Employee { get; set; }


        public int FromId { get; set; }


        public int ToId { get; set; }


         public Employee From { get; set; }
         public Employee To { get; set; }
        public string Text { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }

        public DateTime Date { get; set; }

       

        public virtual ICollection<ItemTransaction> ItemTransactions { get; set; }


        public Order()
        {
            ItemTransactions = new List<ItemTransaction>();
        }
    }
}