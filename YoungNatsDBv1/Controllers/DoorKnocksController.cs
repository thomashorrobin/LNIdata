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
    public class DoorKnocksController : Controller
    {
        private Model1 db = new Model1();

        // GET: DoorKnocks
        public ActionResult Index()
        {
            var doorKnocks = db.DoorKnocks.Include(d => d.Address).Include(d => d.KnownIndividual);
            return View(doorKnocks.ToList());
        }

        // GET: DoorKnocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoorKnock doorKnock = db.DoorKnocks.Find(id);
            if (doorKnock == null)
            {
                return HttpNotFound();
            }
            return View(doorKnock);
        }

        // GET: DoorKnocks/Create
        public ActionResult Create()
        {
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "Address1");
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName");
            return View();
        }

        // POST: DoorKnocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DoorKnockId,KnownIndividualId,Notes,SpokeToSomeoneAtTheAddress,DateAndTime,AddressId")] DoorKnock doorKnock)
        {
            if (ModelState.IsValid)
            {
                db.DoorKnocks.Add(doorKnock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "Address1", doorKnock.AddressId);
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", doorKnock.KnownIndividualId);
            return View(doorKnock);
        }

        // GET: DoorKnocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoorKnock doorKnock = db.DoorKnocks.Find(id);
            if (doorKnock == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "Address1", doorKnock.AddressId);
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", doorKnock.KnownIndividualId);
            return View(doorKnock);
        }

        // POST: DoorKnocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DoorKnockId,KnownIndividualId,Notes,SpokeToSomeoneAtTheAddress,DateAndTime,AddressId")] DoorKnock doorKnock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doorKnock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "Address1", doorKnock.AddressId);
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", doorKnock.KnownIndividualId);
            return View(doorKnock);
        }

        // GET: DoorKnocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoorKnock doorKnock = db.DoorKnocks.Find(id);
            if (doorKnock == null)
            {
                return HttpNotFound();
            }
            return View(doorKnock);
        }

        // POST: DoorKnocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DoorKnock doorKnock = db.DoorKnocks.Find(id);
            db.DoorKnocks.Remove(doorKnock);
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
