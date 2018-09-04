using System.Collections.Generic;
using System.Web.Http;

namespace WebApplication2.Controllers
{
    public class ModelController : ApiController
    {
        private LogisticsEntities db = new LogisticsEntities();
        // GET: api/Model
        public IEnumerable<Model> Get()
        {
            return db.Model;
        }

        // GET: api/Model/5
        public Model Get(int id)
        {
            return db.Model.Find(id);
        }

        // POST: api/Model
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Model/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Model/5
        public void Delete(int id)
        {
        }
    }
}
