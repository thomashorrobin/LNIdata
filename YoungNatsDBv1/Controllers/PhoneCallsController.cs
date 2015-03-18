using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YoungNatsDBv1.DataModels;
using YoungNatsDBv1.Models;

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

        // GET: PhoneCalls/NewCall?AssignedCallId=23
        public ActionResult NewCall(int? AssignedCallId)
        {
            if (AssignedCallId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignedCall assignedCall = db.AssignedCalls.Find(AssignedCallId);
            ViewBag.voters = db.Voters.Where(e => e.HomeNumber == assignedCall.PhoneNumberId).ToList();
            ViewBag.KnownIndividualId = assignedCall.AssignedTo;
            ViewBag.PhoneNumberId = assignedCall.PhoneNumberId;
            ViewBag.CallTime = DateTime.Now;
            ViewBag.number = assignedCall.PhoneNumber.PhoneNumber1;
            return View();
        }

        public void AddVoterAssessment(int? VoterId, int? KnownIndividualId, int? VotingNationalLikelihood, int? PoliticalPartyId, int? VotingLikelihood)
        {
            VoterAssessment voterAssessment = new VoterAssessment() { VoterId = (int)VoterId, KnownIndividualId = (int)KnownIndividualId, VotingNationalLikelihood = (int)VotingNationalLikelihood, VotingLikelihood = (int)VotingLikelihood, AssessmentDate = DateTime.Now };
            db.VoterAssessments.Add(voterAssessment);
            db.SaveChanges();
        }

        // POST: PhoneCalls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewCall([Bind(Include = "PhoneCallId,PhoneNumberId,KnownIndividualId,WasThePhoneAnswered,CallNotes,CallDateTime")] PhoneCall phoneCall)
        {
            if (ModelState.IsValid)
            {
                //return View("Details",phoneCall);
                phoneCall.CallDateTime = DateTime.Now;
                db.PhoneCalls.Add(phoneCall);
                db.SaveChanges();
                return RedirectToAction("MyCalls", "AssignedCalls");
            }

            return Content("Form submission has failed, please contacted Thomas Horrobin for assistance");
            //ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", phoneCall.KnownIndividualId);
            //ViewBag.PhoneNumberId = new SelectList(db.PhoneNumbers, "PhoneNumberId", "PhoneNumber1", phoneCall.PhoneNumberId);
            //return View(phoneCall);
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
