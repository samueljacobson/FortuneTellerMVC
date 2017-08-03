using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FortuneTellerMVC.Models;

namespace FortuneTellerMVC.Controllers
{
    public class CustomersController : Controller
    {
        private FortuneTellerMVCEntities3 db = new FortuneTellerMVCEntities3();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            //Retirement Age
            if (customer.Age % 2 == 0)
                ViewBag.RetirementAge = 10;
            else ViewBag.RetirementAge = 50;
            //Money in bank
            if (customer.BirthMonth >= 1 && customer.BirthMonth <= 4)
            {
                ViewBag.Money = "$1 million";
            }
            else if (customer.BirthMonth >= 5 && customer.BirthMonth <= 8)
            {
                ViewBag.Money = "$10 million";
            }
            else if (customer.BirthMonth >= 9 && customer.BirthMonth <= 12)
            {
                ViewBag.Money = "$1 billion";
            }
            else
            {
                ViewBag.Money = "$0";
            }
            //Vacation Home 
            if (customer.NumberOfSiblings == 0)
            {
                ViewBag.Location = "Palm Beach";
            }
            else if (customer.NumberOfSiblings == 1)
            {
                ViewBag.Location = "Aspen";
            }
            else if (customer.NumberOfSiblings == 2)
            {
                ViewBag.Location = "the Hamptons";
            }
            else if (customer.NumberOfSiblings == 3)
            {
                ViewBag.Location = "Paris";
            }
            else if (customer.NumberOfSiblings >= 4)
            {
                ViewBag.Location = "Hawaii";
            }
            else
            {
                ViewBag.Location = "a trailer park";
            }
            //Transportation Mode
            switch (customer.FavoriteColor)
            {
                case "red":
                    ViewBag.Transportation = "Toyota Camry";
                    break;
                case "orange":
                    ViewBag.Transportation = "yacht";
                    break;
                case "yellow":
                    ViewBag.Transportation = "motorcycle";
                    break;
                case "green":
                    ViewBag.Transportation = "unicycle";
                    break;
                case "blue":
                    ViewBag.Transportation = "pair of feet";
                    break;
                case "indigo":
                    ViewBag.Transportation = "Razor scooter";
                    break;
                case "violet":
                    ViewBag.Transportation = "jet";
                    break;
                default:
                    ViewBag.Transportation = "suspended driver's license because you did not choose a proper ROYGBIV color as your favorite";
                    break;
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonth,FavoriteColor,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonth,FavoriteColor,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
