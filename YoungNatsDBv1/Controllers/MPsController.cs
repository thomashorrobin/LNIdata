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
    public class MPsController : Controller
    {
        private Model1 db = new Model1();

        // GET: MPs
        public ActionResult Index()
        {
            var mPs = db.MPs.Include(m => m.Electorate).Include(m => m.KnownIndividual);
            return View(mPs.ToList());
        }

        // GET: MPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MP mP = db.MPs.Find(id);
            if (mP == null)
            {
                return HttpNotFound();
            }
            return View(mP);
        }

        // GET: MPs/Create
        public ActionResult Create()
        {
            ViewBag.ElectorateId = new SelectList(db.Electorates, "ElectorateId", "ElectorateName");
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName");
            return View();
        }

        // POST: MPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KnownIndividualId,DateFirstBecameMP,ElectorateId")] MP mP)
        {
            if (ModelState.IsValid)
            {
                db.MPs.Add(mP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ElectorateId = new SelectList(db.Electorates, "ElectorateId", "ElectorateName", mP.ElectorateId);
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", mP.KnownIndividualId);
            return View(mP);
        }

        // GET: MPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MP mP = db.MPs.Find(id);
            if (mP == null)
            {
                return HttpNotFound();
            }
            ViewBag.ElectorateId = new SelectList(db.Electorates, "ElectorateId", "ElectorateName", mP.ElectorateId);
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", mP.KnownIndividualId);
            return View(mP);
        }

        // POST: MPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KnownIndividualId,DateFirstBecameMP,ElectorateId")] MP mP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ElectorateId = new SelectList(db.Electorates, "ElectorateId", "ElectorateName", mP.ElectorateId);
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", mP.KnownIndividualId);
            return View(mP);
        }

        // GET: MPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MP mP = db.MPs.Find(id);
            if (mP == null)
            {
                return HttpNotFound();
            }
            return View(mP);
        }

        // POST: MPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MP mP = db.MPs.Find(id);
            db.MPs.Remove(mP);
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
