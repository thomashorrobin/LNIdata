using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml.Linq;
using YoungNatsDBv1.DataModels;

namespace YoungNatsDBv1.Controllers
{
    [RequireHttps]
    [Authorize(Roles="checked")]
    public class HomeController : Controller
    {
        private Model1 db = new Model1();

        [AllowAnonymous]
        public ActionResult Index()
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return Content("not even authenticated");
            //}
            //else if (!User.IsInRole("checked"))
            //{
            //    return Content("not approved");
            //}
            //else
            //{
            //    return View();
            //}
            return View();
        }

        [Authorize(Roles="admin")]
        public ActionResult ManageUsers()
        {
            ViewBag.KnownIndividuals = new SelectList(db.KnownIndividuals, "KnownIndividualId", "FullName");
            return View(db.AspNetUsers);
        }

        [Authorize(Roles="admin")]
        public ActionResult MatchUserToIndividual(int? KnownIndividualId, string id)
        {
            AspNetUser user = db.AspNetUsers.Find(id);
            KnownIndividual knownIdividual = db.KnownIndividuals.Find(KnownIndividualId);
            user.KnownIndividualId = KnownIndividualId;
            db.SaveChanges();
            return Content("Sucessfully added " + knownIdividual.FullName + " to username:" + user.UserName);
        }

        [Authorize(Roles="admin")]
        public ActionResult AllowUserIntoSite(string id)
        {
            AspNetUser user = db.AspNetUsers.Find(id);
            try
            {
                db.Database.SqlQuery<int>("INSERT INTO dbo.AspNetUserRoles(UserId,RoleId) VALUES('" + user.Id + "','tom');").FirstOrDefault();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            return Content("Added " + user.UserName + " to checked role");
        }

    [Authorize(Roles = "checked")]
        public ActionResult Map()
        {
            return View();
        }

    [Authorize(Roles = "checked")]
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

    [Authorize(Roles = "checked")]
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

    [Authorize(Roles = "checked")]
    public string MarkerData2()
    {
        try
        {
            string json = "";
            foreach (DataModels.Address address in db.Addresses.Where(e => e.GeoTag != null))
            {
                string logs = "";
                foreach (Interfaces.IAddressLog item in Address.LogData(address.AddressId).OrderByDescending(e => e.InteractionDate).Take(3))
                {
                    logs += item.GetHtml(new string[0]);
                }
                json += "{ \"marker\": " + address.GetJson() + ", \"infowindow\": { \"content\": \"<h2>" + address.Address1 + "</h2>" + logs + "\" }},";
            }
            json = json.TrimEnd(',');
            return "[" + json + "]";
        }
        catch (Exception ex)
        {
            return ex.Message + " inner exception: "; // + ex.InnerException.Message;
        }
    }

    [Authorize(Roles = "checked")]
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