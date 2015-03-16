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
    [AllowAnonymous]
    public class KnownIndividualsController : Controller
    {
        private Model1 db = new Model1();

        // GET: KnownIndividuals
        public ActionResult Index()
        {
            var knownIndividuals = db.KnownIndividuals.Include(k => k.MP).Include(k => k.NationalPartyMember);
            return View(knownIndividuals.ToList());
        }

        public ActionResult ViewLog(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Interfaces.IKnownIndividualLog> log = new List<Interfaces.IKnownIndividualLog>();
            log.AddRange(db.DoorKnocks.Where(e => e.KnownIndividualId == id));
            log.AddRange(db.PhoneCalls.Where(e => e.KnownIndividualId == id));
            log.AddRange(db.PamphletDeliveries.Where(e => e.KnownIndividualId == id));
            log = log.OrderByDescending(e => e.InteractionDate).ToList();
            ViewBag.log = log;
            ViewBag.name = db.KnownIndividuals.Find(id).ToString();
            return View();
        }

        // GET: KnownIndividuals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KnownIndividual knownIndividual = db.KnownIndividuals.Find(id);
            if (knownIndividual == null)
            {
                return HttpNotFound();
            }
            return View(knownIndividual);
        }

        // GET: KnownIndividuals/Create
        public ActionResult Create()
        {
            ViewBag.KnownIndividualId = new SelectList(db.MPs, "KnownIndividualId", "KnownIndividualId");
            ViewBag.KnownIndividualId = new SelectList(db.NationalPartyMembers, "KnownIndividualId", "KnownIndividualId");
            return View();
        }

        // POST: KnownIndividuals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KnownIndividualId,FirstName,LastName,CurrentNationalPartyMember")] KnownIndividual knownIndividual)
        {
            if (ModelState.IsValid)
            {
                db.KnownIndividuals.Add(knownIndividual);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KnownIndividualId = new SelectList(db.MPs, "KnownIndividualId", "KnownIndividualId", knownIndividual.KnownIndividualId);
            ViewBag.KnownIndividualId = new SelectList(db.NationalPartyMembers, "KnownIndividualId", "KnownIndividualId", knownIndividual.KnownIndividualId);
            return View(knownIndividual);
        }

        // GET: KnownIndividuals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KnownIndividual knownIndividual = db.KnownIndividuals.Find(id);
            if (knownIndividual == null)
            {
                return HttpNotFound();
            }
            ViewBag.KnownIndividualId = new SelectList(db.MPs, "KnownIndividualId", "KnownIndividualId", knownIndividual.KnownIndividualId);
            ViewBag.KnownIndividualId = new SelectList(db.NationalPartyMembers, "KnownIndividualId", "KnownIndividualId", knownIndividual.KnownIndividualId);
            return View(knownIndividual);
        }

        // POST: KnownIndividuals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KnownIndividualId,FirstName,LastName,CurrentNationalPartyMember")] KnownIndividual knownIndividual)
        {
            if (ModelState.IsValid)
            {
                db.Entry(knownIndividual).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KnownIndividualId = new SelectList(db.MPs, "KnownIndividualId", "KnownIndividualId", knownIndividual.KnownIndividualId);
            ViewBag.KnownIndividualId = new SelectList(db.NationalPartyMembers, "KnownIndividualId", "KnownIndividualId", knownIndividual.KnownIndividualId);
            return View(knownIndividual);
        }

        // GET: KnownIndividuals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KnownIndividual knownIndividual = db.KnownIndividuals.Find(id);
            if (knownIndividual == null)
            {
                return HttpNotFound();
            }
            return View(knownIndividual);
        }

        // POST: KnownIndividuals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KnownIndividual knownIndividual = db.KnownIndividuals.Find(id);
            db.KnownIndividuals.Remove(knownIndividual);
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
