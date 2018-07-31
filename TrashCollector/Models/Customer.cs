using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DisplayFormat(DataFormatString = "{0:dddd}")]
        [DataType(DataType.Date)]
        [Display(Name = "Scheduled Pickup Day")]
        public DateTime? PickupDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dddd}")]
        [DataType(DataType.Date)]
        [Display(Name = "Scheduled Special Pickup Day")]
        public DateTime? SpecialPickupDay { get; set; }
        [DisplayFormat(DataFormatString = "{0:dddd}")]
        [DataType(DataType.Date)]
        public DateTime? SuspendPickupStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dddd}")]
        [DataType(DataType.Date)]
        public DateTime? SuspendPickupEndDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Display(Name = "Balance Due")]
        public float? MonthlyBalance { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        //public string ScheduledPickUpDay { get; set; }
        //[Display(Name = "Special Request Pickup Day")]
        //public string SpecialPickUpDay { get; set; }
        //[Display(Name = "Suspend Pickup Start Date")]
        //public string SuspendPickupStartDate { get; set; }
        //[Display(Name = "Suspend Pickup End Date")]
        //public string SuspendPickupEndDate { get; set; }
        //[Display(Name = "Balance Due This Month")]
        //public string MonthlyBalance { get; set; }
        //public Pickup pickup;

    }
}