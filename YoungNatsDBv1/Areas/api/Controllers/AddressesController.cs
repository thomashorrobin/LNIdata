using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YoungNatsDBv1.DataModels;

namespace YoungNatsDBv1.Areas.api.Controllers
{
    public class AddressesController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/Addresses
        public IEnumerable<string> Get()
        {
            //return new string[] { "value1", "value2" };
            List<Address> addresses = db.Addresses.ToList();
            string[] addressesJson = new string[addresses.Count];
            for (int i = 0; i < addresses.Count; i++)
            {
                addressesJson[i] = addresses[i].GetJson();
            }
            return addressesJson;
        }

        // GET: api/Addresses/5
        public string Get(int id)
        {
            Address a = db.Addresses.Find(id);
            return a.GetJson();
        }

        // POST: api/Addresses
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Addresses/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Addresses/5
        public void Delete(int id)
        {
            Address a = db.Addresses.Find(id);
            db.Addresses.Remove(a);
            db.SaveChanges();
        }
    }
}
