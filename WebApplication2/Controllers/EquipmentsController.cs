using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication2;

namespace WebApplication2.Controllers
{
    public class EquipmentsController : ApiController
    {
        private LogisticsEntities db = new LogisticsEntities();

       

        [Route("api/Equipments/getcategoryes")]
        public List<string> GetCategoryes()
        {
            return db.Model.Select(r => r.Category).Distinct().ToList();
        }

        [Route("api/Equipments/{category}")]
        public List<string> GetTypes(string category)
        {
            return db.Model.Where(t => t.Category == category).Select(r => r.EquipmentType).Distinct().ToList();
        }

        [Route("api/Equipments/{category}/{type}")]
        public List<Model> GetModels(string category, string type)
        {
            return db.Model.Where(t => t.Category == category && t.EquipmentType == type).ToList();
        }

        [Route("api/Equipments/{category}/{type}/{idmodel:int}")]
        public List<Equipment> GetEquipments(string category, string type, int idmodel)
        {
            return db.Equipment.Where(t => t.Model.Category == category && t.Model.EquipmentType == type && t.IDModel == idmodel).ToList();
        }
        // GET: api/Equipments
        // public IQueryable<Equipment> GetEquipment()
        //  {
        //     return db.Equipment;
        // }

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