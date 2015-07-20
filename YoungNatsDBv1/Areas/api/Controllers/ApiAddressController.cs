using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace YoungNatsDBv1.Areas.api.Controllers
{
    public class ApiAddressController : ApiController
    {
        // GET: api/ApiAddress
        public IEnumerable<Models.ApiAddress> Get()
        {
            return Models.ApiAddress.GetFiveAddresses();
        }

        // GET: api/ApiAddress/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApiAddress
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ApiAddress/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiAddress/5
        public void Delete(int id)
        {
        }
    }
}
