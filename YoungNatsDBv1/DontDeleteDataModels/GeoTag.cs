using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoungNatsDBv1.DataModels
{
    public partial class GeoTag
    {

        public string GetJson()
        {
            return "{ \"lat\": " + Latitude.ToString() + ", \"lng\": " + Longitude.ToString() + " }";
        }

        public string GetHtml()
        {
            return "<br/><p>" + this.AddressId + " " + this.Latitude + " " + this.Longitude + "</p>";
        }
    }
}
