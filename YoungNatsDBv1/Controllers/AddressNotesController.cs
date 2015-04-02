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
    [Authorize(Roles = "checked")]
    public class AddressNotesController : Controller
    {
        private Model1 db = new Model1();

        // GET: AddressNotes
        public ActionResult Index()
        {
            var addressNotes = db.AddressNotes.Include(a => a.Address).Include(a => a.KnownIndividual);
            return View(addressNotes.ToList());
        }

        // GET: AddressNotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddressNote addressNote = db.AddressNotes.Find(id);
            if (addressNote == null)
            {
                return HttpNotFound();
            }
            return View(addressNote);
        }

        // GET: AddressNotes/Create
        public ActionResult Create()
        {
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "Address1");
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName");
            return View();
        }

        // POST: AddressNotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AddressNoteId,AddressId,NoteText,KnownIndividualId,NoteDate")] AddressNote addressNote)
        {
            if (ModelState.IsValid)
            {
                db.AddressNotes.Add(addressNote);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "Address1", addressNote.AddressId);
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", addressNote.KnownIndividualId);
            return View(addressNote);
        }

        // GET: AddressNotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddressNote addressNote = db.AddressNotes.Find(id);
            if (addressNote == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "Address1", addressNote.AddressId);
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", addressNote.KnownIndividualId);
            return View(addressNote);
        }

        // POST: AddressNotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AddressNoteId,AddressId,NoteText,KnownIndividualId,NoteDate")] AddressNote addressNote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(addressNote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "Address1", addressNote.AddressId);
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", addressNote.KnownIndividualId);
            return View(addressNote);
        }

        // GET: AddressNotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddressNote addressNote = db.AddressNotes.Find(id);
            if (addressNote == null)
            {
                return HttpNotFound();
            }
            return View(addressNote);
        }

        // POST: AddressNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AddressNote addressNote = db.AddressNotes.Find(id);
            db.AddressNotes.Remove(addressNote);
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
