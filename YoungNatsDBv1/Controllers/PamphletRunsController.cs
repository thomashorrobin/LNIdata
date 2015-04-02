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
    public class PamphletRunsController : Controller
    {
        private Model1 db = new Model1();

        // GET: PamphletRuns
        public ActionResult Index()
        {
            return View(db.PamphletRuns.ToList());
        }

        // GET: PamphletRuns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PamphletRun pamphletRun = db.PamphletRuns.Find(id);
            if (pamphletRun == null)
            {
                return HttpNotFound();
            }
            return View(pamphletRun);
        }

        // GET: PamphletRuns/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PamphletRuns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PamphletRunId,DateCreated,PamphletRunNotes,PamphletRunShortTitle")] PamphletRun pamphletRun)
        {
            if (ModelState.IsValid)
            {
                db.PamphletRuns.Add(pamphletRun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pamphletRun);
        }

        // GET: PamphletRuns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PamphletRun pamphletRun = db.PamphletRuns.Find(id);
            if (pamphletRun == null)
            {
                return HttpNotFound();
            }
            return View(pamphletRun);
        }

        // POST: PamphletRuns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PamphletRunId,DateCreated,PamphletRunNotes,PamphletRunShortTitle")] PamphletRun pamphletRun)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pamphletRun).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pamphletRun);
        }

        // GET: PamphletRuns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PamphletRun pamphletRun = db.PamphletRuns.Find(id);
            if (pamphletRun == null)
            {
                return HttpNotFound();
            }
            return View(pamphletRun);
        }

        // POST: PamphletRuns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PamphletRun pamphletRun = db.PamphletRuns.Find(id);
            db.PamphletRuns.Remove(pamphletRun);
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
