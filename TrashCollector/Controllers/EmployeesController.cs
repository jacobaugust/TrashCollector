using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Index(string Id, ApplicationUser applicationUser, Pickup pickup)
        {

            var userId = User.Identity.GetUserId();
            var userCurrent =
                (from u in db.Users
                 where u.Id == userId
                 select u).First();
            DateTime dateTime = DateTime.Today;
            
            var refinedpickups =
                from p in db.pickups
                where p.pickupZipCode == userCurrent.ZipCode && DbFunctions.TruncateTime(p.pickUpDate) == DbFunctions.TruncateTime(dateTime)
                select p;
            return View(refinedpickups);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "employeeZipCode")] Employee employee)
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


                    employee.Name = userCurrent.FirstName;
                    employee.ApplicationUserId = userCurrent.Id;
                    db.employees.Add(employee);
                    db.SaveChanges();
                    
                    return RedirectToAction("index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return RedirectToAction("Details");
        }
        public ActionResult Map(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pickup pickup = db.pickups.Find(id);
            if (pickup == null)
            {
                return HttpNotFound();
            }
            var pickupToUpdate = db.pickups.Find(pickup.Id);
            ViewBag.Address = pickupToUpdate.pickupStreetAddress;
            ViewBag.City = pickupToUpdate.pickupCity;
            ViewBag.State = pickupToUpdate.pickupState;
            ViewBag.ZipCode = pickupToUpdate.pickupZipCode;
            ViewBag.APIKey = Keys.GOOGLEAPIKEY;
            return View();
        }
        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pickup pickup = db.pickups.Find(id);
            if (pickup == null)
            {
                return HttpNotFound();
            }
            return View(pickup);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pickupCompleted, EmployeeID")] ApplicationUser applicationUser, Pickup pickup, Employee employee, Customer customer)
        {
            if (ModelState.IsValid)
            {
                var pickupToUpdate = db.pickups.Find(pickup.Id);
                var userId = User.Identity.GetUserId();
                var userCurrent =
                    (from u in db.Users
                     where u.Id == userId
                     select u).First();

                var employeeToUpdate =
                     (from e in db.employees
                      where e.ApplicationUserId == userCurrent.Id
                      select e).First();

                var customerToUpdate =
                      (from b in db.customers
                      where b.ApplicationUser.StreetAddress == pickupToUpdate.pickupStreetAddress
                      select b).First();

                pickupToUpdate.Employee = employeeToUpdate;
                pickupToUpdate.EmployeeID = employeeToUpdate.Id;
                pickupToUpdate.pickupCompleted = true;
                if (customerToUpdate.MonthlyBalance == null)
                {
                    customerToUpdate.MonthlyBalance = (0 + pickup.PickupChargeAmount);
                }
                else
                {
                    customerToUpdate.MonthlyBalance = (customerToUpdate.MonthlyBalance + pickupToUpdate.PickupChargeAmount);
                }
                db.Entry(customerToUpdate).State = EntityState.Modified;
                db.Entry(pickupToUpdate).State = EntityState.Modified;
                db.SaveChanges();

       
                return RedirectToAction("Index");
            }
            return View(pickup);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.employees.Find(id);
            db.employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
