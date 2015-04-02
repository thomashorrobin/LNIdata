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
    public class NationalPartyMembersController : Controller
    {
        private Model1 db = new Model1();

        // GET: NationalPartyMembers
        public ActionResult Index()
        {
            var nationalPartyMembers = db.NationalPartyMembers.Include(n => n.Electorate).Include(n => n.KnownIndividual);
            return View(nationalPartyMembers.ToList());
        }

        // GET: NationalPartyMembers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalPartyMember nationalPartyMember = db.NationalPartyMembers.Find(id);
            if (nationalPartyMember == null)
            {
                return HttpNotFound();
            }
            return View(nationalPartyMember);
        }

        // GET: NationalPartyMembers/Create
        public ActionResult Create()
        {
            ViewBag.ElectorateId = new SelectList(db.Electorates, "ElectorateId", "ElectorateName");
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName");
            return View();
        }

        // POST: NationalPartyMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KnownIndividualId,MemberSince,ElectorateId")] NationalPartyMember nationalPartyMember)
        {
            if (ModelState.IsValid)
            {
                db.NationalPartyMembers.Add(nationalPartyMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ElectorateId = new SelectList(db.Electorates, "ElectorateId", "ElectorateName", nationalPartyMember.ElectorateId);
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", nationalPartyMember.KnownIndividualId);
            return View(nationalPartyMember);
        }

        // GET: NationalPartyMembers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalPartyMember nationalPartyMember = db.NationalPartyMembers.Find(id);
            if (nationalPartyMember == null)
            {
                return HttpNotFound();
            }
            ViewBag.ElectorateId = new SelectList(db.Electorates, "ElectorateId", "ElectorateName", nationalPartyMember.ElectorateId);
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", nationalPartyMember.KnownIndividualId);
            return View(nationalPartyMember);
        }

        // POST: NationalPartyMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KnownIndividualId,MemberSince,ElectorateId")] NationalPartyMember nationalPartyMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nationalPartyMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ElectorateId = new SelectList(db.Electorates, "ElectorateId", "ElectorateName", nationalPartyMember.ElectorateId);
            ViewBag.KnownIndividualId = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FirstName", nationalPartyMember.KnownIndividualId);
            return View(nationalPartyMember);
        }

        // GET: NationalPartyMembers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalPartyMember nationalPartyMember = db.NationalPartyMembers.Find(id);
            if (nationalPartyMember == null)
            {
                return HttpNotFound();
            }
            return View(nationalPartyMember);
        }

        // POST: NationalPartyMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NationalPartyMember nationalPartyMember = db.NationalPartyMembers.Find(id);
            db.NationalPartyMembers.Remove(nationalPartyMember);
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
