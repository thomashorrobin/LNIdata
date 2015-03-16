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
    public class PhoneCallsController : Controller
    {
        private Model1 db = new Model1();

        // GET: PhoneCalls
        public ActionResult Index()
        {
            var phoneCalls = db.PhoneCalls.Include(p => p.KnownIndividual).Include(p => p.PhoneNumber);
            return View(phoneCalls.ToList());
        }

        // GET: PhoneCalls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneCall phoneCall = db.PhoneCalls.Find(id);
            if (phoneCall == null)
            {
                return HttpNotFound();
            }
            return View(phoneCall);
        }

        // GET: PhoneCalls/Create
        public ActionResult Create()
        {
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName");
            ViewBag.PhoneNumberId = new SelectList(db.PhoneNumbers, "PhoneNumberId", "PhoneNumber1");
            return View();
        }

        // POST: PhoneCalls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhoneCallId,PhoneNumberId,KnownIndividualId,WasThePhoneAnswered,CallNotes,CallDateTime")] PhoneCall phoneCall)
        {
            if (ModelState.IsValid)
            {
                db.PhoneCalls.Add(phoneCall);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", phoneCall.KnownIndividualId);
            ViewBag.PhoneNumberId = new SelectList(db.PhoneNumbers, "PhoneNumberId", "PhoneNumber1", phoneCall.PhoneNumberId);
            return View(phoneCall);
        }

        // GET: PhoneCalls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneCall phoneCall = db.PhoneCalls.Find(id);
            if (phoneCall == null)
            {
                return HttpNotFound();
            }
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", phoneCall.KnownIndividualId);
            ViewBag.PhoneNumberId = new SelectList(db.PhoneNumbers, "PhoneNumberId", "PhoneNumber1", phoneCall.PhoneNumberId);
            return View(phoneCall);
        }

        // POST: PhoneCalls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhoneCallId,PhoneNumberId,KnownIndividualId,WasThePhoneAnswered,CallNotes,CallDateTime")] PhoneCall phoneCall)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phoneCall).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", phoneCall.KnownIndividualId);
            ViewBag.PhoneNumberId = new SelectList(db.PhoneNumbers, "PhoneNumberId", "PhoneNumber1", phoneCall.PhoneNumberId);
            return View(phoneCall);
        }

        // GET: PhoneCalls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneCall phoneCall = db.PhoneCalls.Find(id);
            if (phoneCall == null)
            {
                return HttpNotFound();
            }
            return View(phoneCall);
        }

        // POST: PhoneCalls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhoneCall phoneCall = db.PhoneCalls.Find(id);
            db.PhoneCalls.Remove(phoneCall);
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
