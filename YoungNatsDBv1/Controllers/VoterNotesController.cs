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
    public class VoterNotesController : Controller
    {
        private Model1 db = new Model1();

        // GET: VoterNotes
        public ActionResult Index()
        {
            var voterNotes = db.VoterNotes.Include(v => v.KnownIndividual).Include(v => v.Voter);
            return View(voterNotes.ToList());
        }

        // GET: VoterNotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoterNote voterNote = db.VoterNotes.Find(id);
            if (voterNote == null)
            {
                return HttpNotFound();
            }
            return View(voterNote);
        }

        // GET: VoterNotes/Create
        public ActionResult Create()
        {
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName");
            ViewBag.VoterId = new SelectList(db.Voters, "VoterId", "FirstName");
            return View();
        }

        // POST: VoterNotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VoterNoteId,VoterId,NoteText,KnownIndividualId,NoteDate")] VoterNote voterNote)
        {
            if (ModelState.IsValid)
            {
                db.VoterNotes.Add(voterNote);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", voterNote.KnownIndividualId);
            ViewBag.VoterId = new SelectList(db.Voters, "VoterId", "FirstName", voterNote.VoterId);
            return View(voterNote);
        }

        // GET: VoterNotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoterNote voterNote = db.VoterNotes.Find(id);
            if (voterNote == null)
            {
                return HttpNotFound();
            }
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", voterNote.KnownIndividualId);
            ViewBag.VoterId = new SelectList(db.Voters, "VoterId", "FirstName", voterNote.VoterId);
            return View(voterNote);
        }

        // POST: VoterNotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VoterNoteId,VoterId,NoteText,KnownIndividualId,NoteDate")] VoterNote voterNote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voterNote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", voterNote.KnownIndividualId);
            ViewBag.VoterId = new SelectList(db.Voters, "VoterId", "FirstName", voterNote.VoterId);
            return View(voterNote);
        }

        // GET: VoterNotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VoterNote voterNote = db.VoterNotes.Find(id);
            if (voterNote == null)
            {
                return HttpNotFound();
            }
            return View(voterNote);
        }

        // POST: VoterNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VoterNote voterNote = db.VoterNotes.Find(id);
            db.VoterNotes.Remove(voterNote);
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
