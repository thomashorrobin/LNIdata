using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoungNatsDBv1.DataModels
{
    public partial class Address
    {
        public static List<Interfaces.IAddressLog> LogData(int AddressId)
        {
            Model1 db = new Model1();
            List<Interfaces.IAddressLog> log = new List<Interfaces.IAddressLog>();
            log.AddRange(db.DoorKnocks.Where(e => e.AddressId == AddressId));
            log.AddRange(db.PamphletDeliveries.Where(e => e.AddressId == AddressId));
            return log;
        }

        public string GetJson()
        {
            string icon;
            if (PoliticalLeanings == null)
            {
                icon = "http://maps.google.com/mapfiles/ms/icons/blue-dot.png";
            }
            else if (PoliticalLeanings == 4)
            {
                icon = "http://maps.google.com/mapfiles/ms/icons/blue-dot.png";
            }
            else if (PoliticalLeanings == 5)
            {
                icon = "http://maps.google.com/mapfiles/ms/icons/red-dot.png";
            }
            else if (PoliticalLeanings == 1005)
            {
                icon = "http://maps.google.com/mapfiles/ms/icons/yellow-dot.png";
            }
            else if (PoliticalLeanings == 6)
            {
                icon = "http://maps.google.com/mapfiles/ms/icons/green-dot.png";
            }
            else if (PoliticalLeanings == 1006)
            {
                icon = "http://maps.google.com/mapfiles/ms/icons/purple-dot.png";
            }
            else
            {
                icon = "http://maps.google.com/mapfiles/ms/icons/black-dot.png";
            }
            if (GeoTag == null)
            {
                return "{ \"position\": null, \"title\": \"" + this.Address1 + "\", \"icon\":\"" + icon + "\" }";
            }
            return "{ \"position\": " + GeoTag.GetJson() + ", \"title\": \"" + this.Address1 + "\", \"icon\":\"" + icon + "\" }";
        }
    }
}
