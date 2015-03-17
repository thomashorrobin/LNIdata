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
    public class PoliticalPartiesController : Controller
    {
        private Model1 db = new Model1();

        // GET: PoliticalParties
        public ActionResult Index()
        {
            return View(db.PoliticalParties.ToList());
        }

        // GET: PoliticalParties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliticalParty politicalParty = db.PoliticalParties.Find(id);
            if (politicalParty == null)
            {
                return HttpNotFound();
            }
            return View(politicalParty);
        }

        // GET: PoliticalParties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PoliticalParties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PoliticalPartyId,PartyName")] PoliticalParty politicalParty)
        {
            if (ModelState.IsValid)
            {
                db.PoliticalParties.Add(politicalParty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(politicalParty);
        }

        // GET: PoliticalParties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliticalParty politicalParty = db.PoliticalParties.Find(id);
            if (politicalParty == null)
            {
                return HttpNotFound();
            }
            return View(politicalParty);
        }

        // POST: PoliticalParties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PoliticalPartyId,PartyName")] PoliticalParty politicalParty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(politicalParty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(politicalParty);
        }

        // GET: PoliticalParties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoliticalParty politicalParty = db.PoliticalParties.Find(id);
            if (politicalParty == null)
            {
                return HttpNotFound();
            }
            return View(politicalParty);
        }

        // POST: PoliticalParties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PoliticalParty politicalParty = db.PoliticalParties.Find(id);
            db.PoliticalParties.Remove(politicalParty);
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
