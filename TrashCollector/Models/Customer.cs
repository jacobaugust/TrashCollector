using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Scheduled Weekly Pickup Day")]
        public string ScheduledPickUpDay { get; set; }
        [Display(Name = "Special Request Pickup Day")]
        public string SpecialPickUpDay { get; set; }
        [Display(Name = "Suspend Pickup Start Date")]
        public string SuspendPickupStartDate { get; set; }
        [Display(Name = "Suspend Pickup End Date")]
        public string SuspendPickupEndDate { get; set; }
        [Display(Name = "Balance Due This Month")]
        public string MonthlyBalance { get; set; }
        public Pickup pickup;

    }
}