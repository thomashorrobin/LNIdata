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
    public class VoterAssessmentsController : Controller
    {
        private Model1 db = new Model1();

        // GET: VoterAssessments
        public ActionResult Index()
        {
            var voterAssessments = db.VoterAssessments.Include(v => v.KnownIndividual).Include(v => v.PoliticalParty).Include(v => v.Voter);
            return View(voterAssessments.ToList());
        }

        public PartialViewResult AssessmentPartial(int VoterId, int KnownIndividualId)
        {
            ViewBag.VoterId = VoterId;
            ViewBag.KnownIndividualId = KnownIndividualId;
            return PartialView();
        }

        // GET: VoterAssessments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoterAssessment voterAssessment = db.VoterAssessments.Find(id);
            if (voterAssessment == null)
            {
                return HttpNotFound();
            }
            return View(voterAssessment);
        }

        // GET: VoterAssessments/Create
        public ActionResult Create()
        {
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName");
            ViewBag.PoliticalPartyId = new SelectList(db.PoliticalParties, "PoliticalPartyId", "PartyName");
            ViewBag.VoterId = new SelectList(db.Voters, "VoterId", "FirstName");
            return View();
        }

        // POST: VoterAssessments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VoterAssessmentId,VoterId,KnownIndividualId,AssessmentDate,VotingNationalLikelihood,PoliticalPartyId,VotingLikelihood")] VoterAssessment voterAssessment)
        {
            if (ModelState.IsValid)
            {
                db.VoterAssessments.Add(voterAssessment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", voterAssessment.KnownIndividualId);
            ViewBag.PoliticalPartyId = new SelectList(db.PoliticalParties, "PoliticalPartyId", "PartyName", voterAssessment.PoliticalPartyId);
            ViewBag.VoterId = new SelectList(db.Voters, "VoterId", "FirstName", voterAssessment.VoterId);
            return View(voterAssessment);
        }

        // GET: VoterAssessments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoterAssessment voterAssessment = db.VoterAssessments.Find(id);
            if (voterAssessment == null)
            {
                return HttpNotFound();
            }
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", voterAssessment.KnownIndividualId);
            ViewBag.PoliticalPartyId = new SelectList(db.PoliticalParties, "PoliticalPartyId", "PartyName", voterAssessment.PoliticalPartyId);
            ViewBag.VoterId = new SelectList(db.Voters, "VoterId", "FirstName", voterAssessment.VoterId);
            return View(voterAssessment);
        }

        // POST: VoterAssessments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VoterAssessmentId,VoterId,KnownIndividualId,AssessmentDate,VotingNationalLikelihood,PoliticalPartyId,VotingLikelihood")] VoterAssessment voterAssessment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voterAssessment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", voterAssessment.KnownIndividualId);
            ViewBag.PoliticalPartyId = new SelectList(db.PoliticalParties, "PoliticalPartyId", "PartyName", voterAssessment.PoliticalPartyId);
            ViewBag.VoterId = new SelectList(db.Voters, "VoterId", "FirstName", voterAssessment.VoterId);
            return View(voterAssessment);
        }

        // GET: VoterAssessments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoterAssessment voterAssessment = db.VoterAssessments.Find(id);
            if (voterAssessment == null)
            {
                return HttpNotFound();
            }
            return View(voterAssessment);
        }

        // POST: VoterAssessments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VoterAssessment voterAssessment = db.VoterAssessments.Find(id);
            db.VoterAssessments.Remove(voterAssessment);
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
