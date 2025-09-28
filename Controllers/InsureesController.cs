using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInsurance.Models;

namespace CarInsurance.Controllers
{
    public class InsureesController : Controller
    {
        private InsuranceEntities db = new InsuranceEntities();

        // GET: Insurees
        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }

        // GET: Insurees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // GET: Insurees/Create
        public ActionResult Create()
        {
            return View(new Insuree());

        }

        // POST: Insurees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                decimal quote = 50m;

                // age
                var today = DateTime.Today;
                int age = today.Year - insuree.DateOfBirth.Year;
                if (insuree.DateOfBirth > today.AddYears(-age)) age--;

                if (age <= 18)
                    quote += 100;
                else if (age >= 19 && age <= 25)
                    quote += 50;
                else
                    quote += 25;

                // car year
                if (insuree.CarYear < 2000)
                    quote += 25;
                else if (insuree.CarYear > 2015)
                    quote += 25;

                // car make/model
                if (insuree.CarMake?.ToLower() == "porsche")
                {
                    quote += 25;
                    if (insuree.CarModel?.ToLower() == "911 carrera")
                    {
                        quote += 25;
                    }
                }

                // speeding tickets
                quote += insuree.SpeedingTickets * 10;

                // DUI
                if (insuree.DUI)
                    quote *= 1.25m;

                // coverage type
                if (insuree.CoverageType)
                    quote *= 1.50m;

                insuree.Quote = quote;

                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insuree);
        }



        // GET: Insurees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insurees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                decimal quote = 50m;

                // age
                var today = DateTime.Today;
                int age = today.Year - insuree.DateOfBirth.Year;
                if (insuree.DateOfBirth > today.AddYears(-age)) age--;

                if (age <= 18)
                    quote += 100;
                else if (age >= 19 && age <= 25)
                    quote += 50;
                else
                    quote += 25;

                // car year
                if (insuree.CarYear < 2000)
                    quote += 25;
                else if (insuree.CarYear > 2015)
                    quote += 25;

                // car make/model
                if (insuree.CarMake?.ToLower() == "porsche")
                {
                    quote += 25;
                    if (insuree.CarModel?.ToLower() == "911 carrera")
                    {
                        quote += 25;
                    }
                }

                // speeding tickets
                quote += insuree.SpeedingTickets * 10;

                // DUI
                if (insuree.DUI)
                    quote *= 1.25m;

                // coverage type
                if (insuree.CoverageType)
                    quote *= 1.50m;

                insuree.Quote = quote;


                db.Entry(insuree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        // GET: Insurees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insurees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insuree insuree = db.Insurees.Find(id);
            db.Insurees.Remove(insuree);
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


        public ActionResult Admin()
        {
            var insurees = db.Insurees.ToList();
            return View(insurees);
        }







    }
}
