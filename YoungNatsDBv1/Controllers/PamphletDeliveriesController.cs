using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YoungNatsDBv1.DataModels;

namespace YoungNatsDBv1.Controllers
{
    public class PamphletDeliveriesController : Controller
    {
        private Model1 db = new Model1();

        // GET: PamphletDeliveries
        public ActionResult Index()
        {
            var pamphletDeliveries = db.PamphletDeliveries.Include(p => p.Address).Include(p => p.KnownIndividual).Include(p => p.PamphletRun);
            return View(pamphletDeliveries.ToList());
        }

        // GET: PamphletDeliveries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PamphletDelivery pamphletDelivery = db.PamphletDeliveries.Find(id);
            if (pamphletDelivery == null)
            {
                return HttpNotFound();
            }
            return View(pamphletDelivery);
        }

        // GET: PamphletDeliveries/Create
        public ActionResult Create()
        {
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "Address1");
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName");
            ViewBag.PamphletRunId = new SelectList(db.PamphletRuns, "PamphletRunId", "PamphletRunNotes");
            return View();
        }

        // POST: PamphletDeliveries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PamphletRunId,AddressId,KnownIndividualId,DateTimeDelivered")] PamphletDelivery pamphletDelivery)
        {
            if (ModelState.IsValid)
            {
                db.PamphletDeliveries.Add(pamphletDelivery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "Address1", pamphletDelivery.AddressId);
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", pamphletDelivery.KnownIndividualId);
            ViewBag.PamphletRunId = new SelectList(db.PamphletRuns, "PamphletRunId", "PamphletRunNotes", pamphletDelivery.PamphletRunId);
            return View(pamphletDelivery);
        }

        // GET: PamphletDeliveries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PamphletDelivery pamphletDelivery = db.PamphletDeliveries.Find(id);
            if (pamphletDelivery == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "Address1", pamphletDelivery.AddressId);
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", pamphletDelivery.KnownIndividualId);
            ViewBag.PamphletRunId = new SelectList(db.PamphletRuns, "PamphletRunId", "PamphletRunNotes", pamphletDelivery.PamphletRunId);
            return View(pamphletDelivery);
        }

        // POST: PamphletDeliveries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PamphletRunId,AddressId,KnownIndividualId,DateTimeDelivered")] PamphletDelivery pamphletDelivery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pamphletDelivery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "Address1", pamphletDelivery.AddressId);
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", pamphletDelivery.KnownIndividualId);
            ViewBag.PamphletRunId = new SelectList(db.PamphletRuns, "PamphletRunId", "PamphletRunNotes", pamphletDelivery.PamphletRunId);
            return View(pamphletDelivery);
        }

        // GET: PamphletDeliveries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PamphletDelivery pamphletDelivery = db.PamphletDeliveries.Find(id);
            if (pamphletDelivery == null)
            {
                return HttpNotFound();
            }
            return View(pamphletDelivery);
        }

        // POST: PamphletDeliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PamphletDelivery pamphletDelivery = db.PamphletDeliveries.Find(id);
            db.PamphletDeliveries.Remove(pamphletDelivery);
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
