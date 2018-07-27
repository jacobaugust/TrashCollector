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
                    customer.CustomerName = applicationUser.FirstName;
                    db.customers.Add(customer);
                    customer.PickupDate = pickup.pickUpDate;
                    applicationUser.StreetAddress = pickup.pickupStreetAddress;
                    applicationUser.City = pickup.pickupCity;
                    applicationUser.State = pickup.pickupState;
                    applicationUser.ZipCode = pickup.pickupZipCode;
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
            Customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
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
        public ActionResult Index([Bind(Include = "PickupDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                //Find Customer
                //Input Pickup Date
                db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }

            return View(customer);
        }
        
    }
}