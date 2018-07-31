using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Pickup
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public int? EmployeeID { get; set; }
        public Employee Employee { get; set; }
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public float? PickupChargeAmount = 20;
        public static List<string> pickupDays = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display (Name = "Scheduled Pickup Date")]
        public DateTime? pickUpDate { get; set; }
        [Display(Name = "Pickup Completion Status")]
        public bool pickupCompleted { get; set; }
        [Display(Name = "Street Address")]
        public string pickupStreetAddress { get; set; }
        [Display(Name = "City")]
        public string pickupCity { get; set; }
        [Display(Name = "State")]
        public string pickupState { get; set; }
        [Display(Name = "Zip Code")]
        public string pickupZipCode { get; set; }
       

    }
}