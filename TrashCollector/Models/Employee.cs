using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Employee
    {
        [Key]
        public int? Id { get; set; }
        [Display(Name = "Employee Name")]
        public string Name { get; set; }
        [Display(Name = "Employee Zip Code")]
        public string employeeZipCode { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        //Filter Customers (in thier pickup area/by each day of the week see who gets a pickup on that date)
        //DataGridView?
        //Form to enter completed pickups
        //Apply charge for pickup
        //Customer Select and display address on may via Google API
        public virtual ICollection<Pickup> Pickups { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}