using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using YoungNatsDBv1.DataModels;

namespace YoungNatsDBv1.Controllers
{
    [RequireHttps]
    [Authorize(Roles="checked")]
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
                return ex.Message + " inner exception: "; // + ex.InnerException.Message;
            }
        }

        public ActionResult DownloadAddresses(int numberToRun)
        {
            StringBuilder text = new StringBuilder();
            text.AppendLine("Lats and long");
            List<Address> addresses = db.Addresses.Where(e => e.GeoTagAddressId == null).Take(numberToRun).ToList();
            foreach (Address address in addresses)
            {
                string addressString = address.Address1.Replace(' ', '+');
                addressString += "+Wellington";
                WebRequest webRequest = WebRequest.Create("http://maps.googleapis.com/maps/api/geocode/xml?sensor=false&address=" + addressString);

                //text.AppendLine(webRequest.RequestUri.ToString());

                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    using (Stream stream = webResponse.GetResponseStream())
                    {
                        XDocument document = XDocument.Load(new StreamReader(stream));

                        XElement longitudeElement = document.Descendants("lng").FirstOrDefault();
                        XElement latitudeElement = document.Descendants("lat").FirstOrDefault();

                        //text.AppendLine(document.ToString());

                        if (longitudeElement != null && latitudeElement != null)
                        {
                            //text.AppendLine(address.AddressId + ", " + latitudeElement.Value.ToString() + ", " + longitudeElement.Value.ToString());
                            GeoTag geoTag = new GeoTag { AddressId = address.AddressId, Latitude = double.Parse(latitudeElement.Value.ToString()), Longitude = double.Parse(longitudeElement.Value.ToString()) };
                            text.AppendLine(geoTag.GetHtml());
                            db.GeoTags.Add(geoTag);
                            address.GeoTagAddressId = geoTag.AddressId;
                        }
                    }
                }
            }
            db.SaveChanges();
            return Content(text.ToString());
        }
    }
}