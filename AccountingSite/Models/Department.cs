using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AccountingSite.Models
{
    public class Department
    {
        [Key]
        [ForeignKey("Employee")]
        public int Id { get; set; }
        public string Name { get; set; }

        public Employee Employee { get; set; }
    }
}