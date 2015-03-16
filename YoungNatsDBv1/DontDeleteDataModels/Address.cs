using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoungNatsDBv1.DataModels
{
    public partial class Address
    {

        public string GetJson()
        {
            if (GeoTag == null)
            {
                return "{}";
            }
            string icon;
            if (PoliticalLeanings.ToUpper().Contains("NATIONAL"))
            {
                icon = "http://maps.google.com/mapfiles/ms/icons/blue-dot.png";
            }
            else if (PoliticalLeanings.ToUpper().Contains("LABOUR"))
            {
                icon = "http://maps.google.com/mapfiles/ms/icons/red-dot.png";
            }
            else if (PoliticalLeanings.ToUpper().Contains("ACT"))
            {
                icon = "http://maps.google.com/mapfiles/ms/icons/yellow-dot.png";
            }
            else if (PoliticalLeanings.ToUpper().Contains("GREEN"))
            {
                icon = "http://maps.google.com/mapfiles/ms/icons/green-dot.png";
            }
            else if (PoliticalLeanings.ToUpper().Contains("UNITED FUTURE"))
            {
                icon = "http://maps.google.com/mapfiles/ms/icons/purple-dot.png";
            }
            else
            {
                icon = "http://maps.google.com/mapfiles/ms/icons/white-dot.png";
            }
            return "{ \"position\": " + GeoTag.GetJson() + ", \"title\": \"" + this.Address1 + "\", \"icon\":\"" + icon + "\" }";
        }
    }
}
