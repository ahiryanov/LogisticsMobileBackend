﻿using System.Collections.Generic;
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
            List<ModelCount> modelCountList = new List<ModelCount>();
            var equipmentByPosition = db.Equipment.Where(eq => eq.PositionState == position);
            var modelList = equipmentByPosition.Select(r => r.Model).Distinct();
            foreach (var model in modelList)
                modelCountList.Add(new ModelCount()
                {
                    Model = model,
                    Count = equipmentByPosition.Where(eq => eq.Model.IDModel == model.IDModel).Count()
                });
            if (modelCountList.Count == 0)
                return NotFound();
            return Ok(modelCountList);
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
