using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebApplication2.Controllers
{
    public class AuthController : ApiController
    {
        LogisticsEntities db = new LogisticsEntities();
        // GET: api/Auth
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Auth/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Auth
        [ResponseType(typeof(Manager))]
        public IHttpActionResult PostAuth([FromBody]Manager user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Manager authUser = db.Manager.Where(m => m.family == user.family && m.name == user.name && m.password == user.password).FirstOrDefault();
            if (authUser != null)
                return Ok(authUser);
            return NotFound();
        }

        // PUT: api/Auth/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Auth/5
        public void Delete(int id)
        {
        }
    }
}
