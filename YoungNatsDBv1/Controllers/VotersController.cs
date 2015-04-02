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
    public class VotersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Voters
        public ActionResult Index()
        {
            var voters = db.Voters.Include(v => v.Address).Include(v => v.Electorate).Include(v => v.PhoneNumber).Include(v => v.PhoneNumber1);
            return View(voters.ToList());
        }

        public ActionResult ViewLog(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voter voter = db.Voters.Find(id);
            List<Interfaces.IVoterLog> log = new List<Interfaces.IVoterLog>();
            foreach (DoorKnock doorKnock in db.DoorKnocks)
            {
                if (doorKnock.RelatesToVoter((int)id))
                {
                    log.Add(doorKnock);
                }
            }
            foreach (PhoneCall phoneCall in db.PhoneCalls)
            {
                if (phoneCall.RelatesToVoter((int)id))
                {
                    log.Add(phoneCall);
                }
            }
            foreach (PamphletDelivery pamphlet in db.PamphletDeliveries)
            {
                if (pamphlet.RelatesToVoter((int)id))
                {
                    log.Add(pamphlet);
                }
            }
            log = log.OrderByDescending(e => e.InteractionDate).ToList();
            ViewBag.log = log;
            ViewBag.name = voter.FirstName + " " + voter.LastName;
            ViewBag.address = voter.Address.Address1;
            return View();
        }

        // GET: Voters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voter voter = db.Voters.Find(id);
            if (voter == null)
            {
                return HttpNotFound();
            }
            return View(voter);
        }

        // GET: Voters/Create
        public ActionResult Create()
        {
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "Address1");
            ViewBag.ElectorateId = new SelectList(db.Electorates, "ElectorateId", "ElectorateName");
            ViewBag.CellNumber = new SelectList(db.PhoneNumbers, "PhoneNumberId", "PhoneNumber1");
            ViewBag.HomeNumber = new SelectList(db.PhoneNumbers, "PhoneNumberId", "PhoneNumber1");
            return View();
        }

        // POST: Voters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VoterId,FirstName,LastName,AddressId,ElectorateId,Email,PoliticalLeanings,CellNumber,HomeNumber")] Voter voter)
        {
            if (ModelState.IsValid)
            {
                db.Voters.Add(voter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "Address1", voter.AddressId);
            ViewBag.ElectorateId = new SelectList(db.Electorates, "ElectorateId", "ElectorateName", voter.ElectorateId);
            ViewBag.CellNumber = new SelectList(db.PhoneNumbers, "PhoneNumberId", "PhoneNumber1", voter.CellNumber);
            ViewBag.HomeNumber = new SelectList(db.PhoneNumbers, "PhoneNumberId", "PhoneNumber1", voter.HomeNumber);
            return View(voter);
        }

        // GET: Voters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voter voter = db.Voters.Find(id);
            if (voter == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "Address1", voter.AddressId);
            ViewBag.ElectorateId = new SelectList(db.Electorates, "ElectorateId", "ElectorateName", voter.ElectorateId);
            ViewBag.CellNumber = new SelectList(db.PhoneNumbers, "PhoneNumberId", "PhoneNumber1", voter.CellNumber);
            ViewBag.HomeNumber = new SelectList(db.PhoneNumbers, "PhoneNumberId", "PhoneNumber1", voter.HomeNumber);
            return View(voter);
        }

        // POST: Voters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VoterId,FirstName,LastName,AddressId,ElectorateId,Email,PoliticalLeanings,CellNumber,HomeNumber")] Voter voter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "Address1", voter.AddressId);
            ViewBag.ElectorateId = new SelectList(db.Electorates, "ElectorateId", "ElectorateName", voter.ElectorateId);
            ViewBag.CellNumber = new SelectList(db.PhoneNumbers, "PhoneNumberId", "PhoneNumber1", voter.CellNumber);
            ViewBag.HomeNumber = new SelectList(db.PhoneNumbers, "PhoneNumberId", "PhoneNumber1", voter.HomeNumber);
            return View(voter);
        }

        // GET: Voters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voter voter = db.Voters.Find(id);
            if (voter == null)
            {
                return HttpNotFound();
            }
            return View(voter);
        }

        // POST: Voters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Voter voter = db.Voters.Find(id);
            db.Voters.Remove(voter);
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
