using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebApplication2.Controllers
{
    public class EquipmentsController : ApiController
    {
        private LogisticsEntities db = new LogisticsEntities();

        [Route("api/Equipments/getpositions")]
        public List<string> GetPositions()
        {
            return db.Equipment.Select(t => t.PositionState).Distinct().ToList();
        }

        [Route("api/Equipments/gethealths")]
        public List<string> GetHealths()
        {
            return db.Equipment.Select(t => t.HealthState).Distinct().ToList();
        }

        [Route("api/Equipments/{id}/history")]
        public List<TransferEquipment> GetHistory(int id)
        {
            return db.TransferEquipment.Where(t => t.idEquipment == id).ToList();
        }

        [Route("api/Equipments/getcategories")]
        public List<string> GetCategories()
        {
            return db.Model.Select(r => r.Category).Distinct().ToList();
        }

        [Route("api/Equipments/{category}")]
        public List<string> GetTypes(string category)
        {
            return db.Model.Where(t => t.Category == category).Select(r => r.EquipmentType).Distinct().ToList();
        }

        [Route("api/Equipments/{category}/{type}")]
        public List<ModelCount> GetModels(string category, string type)
        {
            return (from model in db.Model
                   where model.Category == category && model.EquipmentType == type
                   select new ModelCount
                   {
                       Model = model,
                       Count = model.Equipment.Count()
                   }).ToList();
        }

        [Route("api/Equipments/{category}/{type}/count")]
        public List<int> GetModelsCount(string category, string type)
        {
            return db.Model.Where(t => t.Category == category && t.EquipmentType == type).Select(r=>r.Equipment.Count).ToList();
        }

        [Route("api/Equipments/{category}/{type}/{idmodel:int}")]
        public List<Equipment> GetEquipments(string category, string type, int idmodel)
        {
            return db.Equipment.Where(t => t.Model.Category == category && t.Model.EquipmentType == type && t.IDModel == idmodel).ToList();
        }
        
        // GET: api/Equipments/5
        [ResponseType(typeof(Equipment))]
        [Route("api/Equipments/{id:int}")]
        public IHttpActionResult GetEquipment(int id)
        {
            Equipment equipment = db.Equipment.Find(id);
            if (equipment == null)
            {
                return NotFound();
            }

            return Ok(equipment);
        }

        // PUT: api/Equipments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEquipment(int id, Equipment equipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != equipment.IDEquipment)
            {
                return BadRequest();
            }

            db.Entry(equipment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Equipments
        [ResponseType(typeof(Equipment))]
        public IHttpActionResult PostEquipment(Equipment equipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Equipment.Add(equipment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EquipmentExists(equipment.IDEquipment))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = equipment.IDEquipment }, equipment);
        }

        // DELETE: api/Equipments/5
        [ResponseType(typeof(Equipment))]
        public IHttpActionResult DeleteEquipment(int id)
        {
            Equipment equipment = db.Equipment.Find(id);
            if (equipment == null)
            {
                return NotFound();
            }

            db.Equipment.Remove(equipment);
            db.SaveChanges();

            return Ok(equipment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EquipmentExists(int id)
        {
            return db.Equipment.Count(e => e.IDEquipment == id) > 0;
        }
    }
}