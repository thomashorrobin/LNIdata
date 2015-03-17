using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YoungNatsDBv1.DataModels;

namespace YoungNatsDBv1.Controllers
{
    [RequireHttps]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private Model1 db = new Model1();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Map()
        {
            return View();
        }

        public ActionResult ManageDatabase()
        {
            ViewBag.AddressesCount = db.Addresses.Count();
            ViewBag.AddressNotesCount = db.AddressNotes.Count();
            ViewBag.AssignedCallsCount = db.AssignedCalls.Count();
            ViewBag.DoorKnocksCount = db.DoorKnocks.Count();
            ViewBag.ElectoratesCount = db.Electorates.Count();
            ViewBag.GeoTagsCount = db.GeoTags.Count();
            ViewBag.KnownIndividualsCount = db.KnownIndividuals.Count();
            ViewBag.MPsCount = db.MPs.Count();
            ViewBag.NationalPartyMembersCount = db.NationalPartyMembers.Count();
            ViewBag.PamphletDeliveriesCount = db.PamphletDeliveries.Count();
            ViewBag.PamphletRunsCount = db.PamphletRuns.Count();
            ViewBag.PhoneCallsCount = db.PhoneCalls.Count();
            ViewBag.PhoneNumbersCount = db.PhoneNumbers.Count();
            ViewBag.PoliticalPartiesCount = db.PoliticalParties.Count();
            ViewBag.VoterAssessmentsCount = db.VoterAssessments.Count();
            ViewBag.VoterNotesCount = db.VoterNotes.Count();
            ViewBag.VotersCount = db.Voters.Count();
            return View();
        }

        public string MarkerData()
        {
            try
            {
                string json = "";
                foreach (DataModels.Address address in db.Addresses.Where(e => e.GeoTag != null))
                {
                    json += address.GetJson() + ",";
                }
                json = json.TrimEnd(',');
                return "[" + json + "]";
            }
            catch (Exception ex)
            {
                return ex.Message + " inner exception: " + ex.InnerException.Message;
            }
        }
    }
}