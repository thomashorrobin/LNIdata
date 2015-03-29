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
    public class AddressesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Addresses
        public ActionResult Index()
        {
            var addresses = db.Addresses.Include(a => a.Electorate).Include(a => a.GeoTag).Include(a => a.GeoTag1);
            return View(addresses.ToList());
        }

        public ActionResult ViewLog(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            List<Interfaces.IAddressLog> log = new List<Interfaces.IAddressLog>();
            log.AddRange(db.DoorKnocks.Where(e => e.AddressId == id));
            log.AddRange(db.PamphletDeliveries.Where(e => e.AddressId == id));
            //log.AddRange(db.PhoneCalls.Where(e => e.AddressId == id));
            ViewBag.log = log;
            ViewBag.address = address.Address1;
            List<string> voters = new List<string>();
            foreach (Voter voter in db.Voters.Where(e => e.AddressId == id))
            {
                voters.Add(voter.FullName);
            }
            ViewBag.voters = voters;
            return View();
        }

        // GET: Addresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // GET: Addresses/Create
        public ActionResult Create()
        {
            ViewBag.ElectorateId = new SelectList(db.Electorates, "ElectorateId", "ElectorateName");
            ViewBag.GeoTagAddressId = new SelectList(db.GeoTags, "AddressId", "AddressId");
            ViewBag.AddressId = new SelectList(db.GeoTags, "AddressId", "AddressId");
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AddressId,Address1,ElectorateId,PoliticalLeanings,GeoTagAddressId")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Addresses.Add(address);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ElectorateId = new SelectList(db.Electorates, "ElectorateId", "ElectorateName", address.ElectorateId);
            ViewBag.GeoTagAddressId = new SelectList(db.GeoTags, "AddressId", "AddressId", address.GeoTagAddressId);
            ViewBag.AddressId = new SelectList(db.GeoTags, "AddressId", "AddressId", address.AddressId);
            return View(address);
        }

        // GET: Addresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            ViewBag.ElectorateId = new SelectList(db.Electorates, "ElectorateId", "ElectorateName", address.ElectorateId);
            ViewBag.GeoTagAddressId = new SelectList(db.GeoTags, "AddressId", "AddressId", address.GeoTagAddressId);
            ViewBag.AddressId = new SelectList(db.GeoTags, "AddressId", "AddressId", address.AddressId);
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AddressId,Address1,ElectorateId,PoliticalLeanings,GeoTagAddressId")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ElectorateId = new SelectList(db.Electorates, "ElectorateId", "ElectorateName", address.ElectorateId);
            ViewBag.GeoTagAddressId = new SelectList(db.GeoTags, "AddressId", "AddressId", address.GeoTagAddressId);
            ViewBag.AddressId = new SelectList(db.GeoTags, "AddressId", "AddressId", address.AddressId);
            return View(address);
        }

        // GET: Addresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Address address = db.Addresses.Find(id);
            db.Addresses.Remove(address);
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
