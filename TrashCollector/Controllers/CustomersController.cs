using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Customers



        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PickupDate")]Customer customer, ApplicationUser applicationUser, Pickup pickup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = User.Identity.GetUserId();
                    var userCurrent =
                        (from u in db.Users
                         where u.Id == userId
                         select u).First();
                    

                    customer.CustomerName = userCurrent.FirstName;
                    customer.MonthlyBalance = (0 + pickup.PickupChargeAmount);
                    customer.ApplicationUserId = userId;
                    db.customers.Add(customer);
                    db.SaveChanges();
                    pickup.CustomerID = customer.Id;
                    pickup.pickUpDate = customer.PickupDate;
                    pickup.pickupStreetAddress = userCurrent.StreetAddress;
                    pickup.pickupCity = userCurrent.City;
                    pickup.pickupState = userCurrent.State;
                    pickup.pickupZipCode = userCurrent.ZipCode;
                    db.pickups.Add(pickup);
                    db.SaveChanges();
                    return RedirectToAction("Details");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return RedirectToAction("Details");
        }

        public ActionResult Details()
        {
            string id = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ViewBag.CustomerID = new SelectList(db.customers, "Id", "CustomerName", pickup.CustomerID);
            //ViewBag.EmployeeID = new SelectList(db.employees, "Id", "Name", pickup.EmployeeID);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "PickupDate")] Customer customer, Pickup pickup)
        {
            if (ModelState.IsValid)
            {
                //Find Customer
                var customerToUpdate = db.customers.Find(customer.ApplicationUserId);
                customerToUpdate.PickupDate = customer.PickupDate;
                db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                var pickupToUpdate = db.pickups.Find(pickup.Id);
                pickupToUpdate.pickUpDate = customer.PickupDate;
                db.Entry(pickup).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }

            return View(customer);
        }
        
    }
}