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
    public class ElectoratesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Electorates
        public ActionResult Index()
        {
            return View(db.Electorates.ToList());
        }

        // GET: Electorates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Electorate electorate = db.Electorates.Find(id);
            if (electorate == null)
            {
                return HttpNotFound();
            }
            return View(electorate);
        }

        // GET: Electorates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Electorates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ElectorateId,ElectorateName")] Electorate electorate)
        {
            if (ModelState.IsValid)
            {
                db.Electorates.Add(electorate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(electorate);
        }

        // GET: Electorates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Electorate electorate = db.Electorates.Find(id);
            if (electorate == null)
            {
                return HttpNotFound();
            }
            return View(electorate);
        }

        // POST: Electorates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ElectorateId,ElectorateName")] Electorate electorate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(electorate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(electorate);
        }

        // GET: Electorates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Electorate electorate = db.Electorates.Find(id);
            if (electorate == null)
            {
                return HttpNotFound();
            }
            return View(electorate);
        }

        // POST: Electorates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Electorate electorate = db.Electorates.Find(id);
            db.Electorates.Remove(electorate);
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
