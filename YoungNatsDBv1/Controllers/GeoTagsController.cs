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
    public class GeoTagsController : Controller
    {
        private Model1 db = new Model1();

        // GET: GeoTags
        public ActionResult Index()
        {
            var geoTags = db.GeoTags.Include(g => g.Address);
            return View(geoTags.ToList());
        }

        // GET: GeoTags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeoTag geoTag = db.GeoTags.Find(id);
            if (geoTag == null)
            {
                return HttpNotFound();
            }
            return View(geoTag);
        }

        // GET: GeoTags/Create
        public ActionResult Create()
        {
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "Address1");
            return View();
        }

        // POST: GeoTags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AddressId,Latitude,Longitude")] GeoTag geoTag)
        {
            if (ModelState.IsValid)
            {
                db.GeoTags.Add(geoTag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "Address1", geoTag.AddressId);
            return View(geoTag);
        }

        // GET: GeoTags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeoTag geoTag = db.GeoTags.Find(id);
            if (geoTag == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "Address1", geoTag.AddressId);
            return View(geoTag);
        }

        // POST: GeoTags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AddressId,Latitude,Longitude")] GeoTag geoTag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(geoTag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "Address1", geoTag.AddressId);
            return View(geoTag);
        }

        // GET: GeoTags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeoTag geoTag = db.GeoTags.Find(id);
            if (geoTag == null)
            {
                return HttpNotFound();
            }
            return View(geoTag);
        }

        // POST: GeoTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GeoTag geoTag = db.GeoTags.Find(id);
            db.GeoTags.Remove(geoTag);
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
