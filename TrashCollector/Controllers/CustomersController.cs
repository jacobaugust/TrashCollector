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
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "PickupDate")] Customer customer, ApplicationUser applicationUser, Pickup pickup)
        {
            if (ModelState.IsValid)
            {
                //Find Customer
                var userId = User.Identity.GetUserId();
                var userCurrent =
                    (from u in db.Users
                     where u.Id == userId
                     select u).First();

                var customerToUpdate =
                     (from c in db.customers
                      where c.ApplicationUserId == userCurrent.Id
                      select c).First();

                customerToUpdate.PickupDate = customer.PickupDate;
                db.Entry(customerToUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                var pickupToUpdate =
                    (from p in db.pickups
                     where p.CustomerID == customerToUpdate.Id
                     select p).First();
                pickupToUpdate.pickUpDate = customerToUpdate.PickupDate;
                db.Entry(pickupToUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }

            return View();
        }
        public ActionResult Suspension()
        {
            string id = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Suspension([Bind(Include = "SuspendPickupStartDate, SuspendPickupEndDate")] Customer customer, ApplicationUser applicationUser, Pickup pickup)
        {
            if (ModelState.IsValid)
            {
                //Find Customer
                var userId = User.Identity.GetUserId();
                var userCurrent =
                    (from u in db.Users
                     where u.Id == userId
                     select u).First();

                var customerToUpdate =
                     (from c in db.customers
                      where c.ApplicationUserId == userCurrent.Id
                      select c).First();

                customerToUpdate.SuspendPickupStartDate = customer.SuspendPickupStartDate;
                customerToUpdate.SuspendPickupEndDate = customer.SuspendPickupEndDate;
                db.Entry(customerToUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Details");
            }

            return View();
        }
        public ActionResult Balance()
        {
            var userId = User.Identity.GetUserId();
            var userCurrent =
                (from u in db.Users
                 where u.Id == userId
                 select u).First();

            var customerToView =
                 (from c in db.customers
                  where c.ApplicationUserId == userCurrent.Id
                  select c).First();
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (customerToView == null)
            {
                return HttpNotFound();
            }
            return View(customerToView);

        }
        public ActionResult Extra()
        {
            string id = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Extra([Bind(Include = "SpecialPickupDay")] Customer customer, ApplicationUser applicationUser, Pickup pickup)
        {
            if (ModelState.IsValid)
            {
                //Find Customer
                var userId = User.Identity.GetUserId();
                var userCurrent =
                    (from u in db.Users
                     where u.Id == userId
                     select u).First();

                var customerToUpdate =
                     (from c in db.customers
                      where c.ApplicationUserId == userCurrent.Id
                      select c).First();

                customerToUpdate.SpecialPickupDay = customer.SpecialPickupDay;
                
                db.Entry(customerToUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                pickup.CustomerID = customerToUpdate.Id;
                pickup.pickUpDate = customerToUpdate.SpecialPickupDay;
                pickup.pickupStreetAddress = userCurrent.StreetAddress;
                pickup.pickupCity = userCurrent.City;
                pickup.pickupState = userCurrent.State;
                pickup.pickupZipCode = userCurrent.ZipCode;
                db.pickups.Add(pickup);
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            

         

                return View(customer);
        }

    }
}