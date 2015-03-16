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