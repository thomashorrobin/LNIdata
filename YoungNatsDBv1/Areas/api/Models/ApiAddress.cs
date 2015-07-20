using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoungNatsDBv1.Areas.api.Models
{
    public class ApiAddress
    {
        public static List<ApiAddress> GetFiveAddresses()
        {
            List<ApiAddress> addrs = new List<ApiAddress>();
            DataModels.Model1 db = new DataModels.Model1();
            foreach (DataModels.Address item in db.Addresses.Take(5).ToList())
            {
                ApiAddress a = new ApiAddress() { Address = item.Address1, dt = DateTime.Now };
                List<string> residents = new List<string>();
                foreach (DataModels.Voter voter in item.Voters)
                {
                    residents.Add(voter.FullName);
                }
                a.Residents = residents;
                addrs.Add(a);
            }
            return addrs;
        }

        public string Address { get; set; }

        public List<string> Residents { get; set; }

        public DateTime dt { get; set; }
    }
}
