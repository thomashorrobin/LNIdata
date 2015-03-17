using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using YoungNatsDBv1.DataModels;

namespace YoungNatsDBv1.Controllers
{
    public class AssignedCallsController : Controller
    {
        private Model1 db = new Model1();

        // GET: AssignedCalls
        public ActionResult Index()
        {
            var assignedCalls = db.AssignedCalls.Include(a => a.KnownIndividual).Include(a => a.KnownIndividual1).Include(a => a.PhoneCall).Include(a => a.PhoneNumber);
            return View(assignedCalls.ToList());
        }

        // GET: AssignedCalls/MyCalls
        public ActionResult MyCalls()
        {
            AspNetUser user = db.AspNetUsers.Single(e => e.UserName == User.Identity.Name);
            KnownIndividual knownIndividual = db.KnownIndividuals.Find(user.KnownIndividualId);
            return Content(knownIndividual.FullName);
        }

        // GET: AssignedCalls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignedCall assignedCall = db.AssignedCalls.Find(id);
            if (assignedCall == null)
            {
                return HttpNotFound();
            }
            return View(assignedCall);
        }

        // GET: AssignedCalls/Create
        public ActionResult Create()
        {
            ViewBag.AssignedBy = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName");
            ViewBag.AssignedTo = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName");
            ViewBag.PhoneCallId = new SelectList(db.PhoneCalls, "PhoneCallId", "CallNotes");
            ViewBag.PhoneNumberId = new SelectList(db.PhoneNumbers, "PhoneNumberId", "PhoneNumber1");
            return View();
        }

        // POST: AssignedCalls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssignedCallId,AssignedBy,AssignedTo,PhoneNumberId,DateTimeAssigned,PhoneCallId,CallCompleted")] AssignedCall assignedCall)
        {
            if (ModelState.IsValid)
            {
                db.AssignedCalls.Add(assignedCall);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssignedBy = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", assignedCall.AssignedBy);
            ViewBag.AssignedTo = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", assignedCall.AssignedTo);
            ViewBag.PhoneCallId = new SelectList(db.PhoneCalls, "PhoneCallId", "CallNotes", assignedCall.PhoneCallId);
            ViewBag.PhoneNumberId = new SelectList(db.PhoneNumbers, "PhoneNumberId", "PhoneNumber1", assignedCall.PhoneNumberId);
            return View(assignedCall);
        }

        // GET: AssignedCalls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignedCall assignedCall = db.AssignedCalls.Find(id);
            if (assignedCall == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssignedBy = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", assignedCall.AssignedBy);
            ViewBag.AssignedTo = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", assignedCall.AssignedTo);
            ViewBag.PhoneCallId = new SelectList(db.PhoneCalls, "PhoneCallId", "CallNotes", assignedCall.PhoneCallId);
            ViewBag.PhoneNumberId = new SelectList(db.PhoneNumbers, "PhoneNumberId", "PhoneNumber1", assignedCall.PhoneNumberId);
            return View(assignedCall);
        }

        // POST: AssignedCalls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssignedCallId,AssignedBy,AssignedTo,PhoneNumberId,DateTimeAssigned,PhoneCallId,CallCompleted")] AssignedCall assignedCall)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignedCall).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssignedBy = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", assignedCall.AssignedBy);
            ViewBag.AssignedTo = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", assignedCall.AssignedTo);
            ViewBag.PhoneCallId = new SelectList(db.PhoneCalls, "PhoneCallId", "CallNotes", assignedCall.PhoneCallId);
            ViewBag.PhoneNumberId = new SelectList(db.PhoneNumbers, "PhoneNumberId", "PhoneNumber1", assignedCall.PhoneNumberId);
            return View(assignedCall);
        }

        // GET: AssignedCalls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignedCall assignedCall = db.AssignedCalls.Find(id);
            if (assignedCall == null)
            {
                return HttpNotFound();
            }
            return View(assignedCall);
        }

        // POST: AssignedCalls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignedCall assignedCall = db.AssignedCalls.Find(id);
            db.AssignedCalls.Remove(assignedCall);
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
