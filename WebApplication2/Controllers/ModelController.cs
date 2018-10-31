using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class ModelController : ApiController
    {
        private LogisticsEntities db = new LogisticsEntities();
        // GET: api/Model
        [Route("api/Model/GetModelsByPosition")]
        [ResponseType(typeof(List<ModelCount>))]
        public IHttpActionResult PostGetModelsByPosition([FromBody]string position)
        {
            return Ok(db.Equipment.
                Where(eq => eq.PositionState == position).
                GroupBy(p => p.Model).
                Select(p => new ModelCount() { Model = p.Key, Count = p.Count() }));
        }

        // GET: api/Model/5
        [Route("api/Model/{id:int}")]
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
