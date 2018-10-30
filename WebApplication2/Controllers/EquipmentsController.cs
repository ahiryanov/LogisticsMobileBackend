using LogisticsMobile;
using System;
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
    [Authorize]
    public class EquipmentsController : ApiController
    {
        private LogisticsEntities db = new LogisticsEntities();

        [Route("api/Equipments/getpositions")]
        public List<string> GetPositions()
        {
            return db.Equipment.Select(t => t.PositionState).Where(t => t != null).Distinct().ToList();
        }

        [Route("api/Equipments/gethealths")]
        public List<string> GetHealths()
        {
            return db.Equipment.Select(t => t.HealthState).Where(t => t != null).Distinct().ToList();
        }

        [Route("api/Equipments/{id:int}/history")]
        public List<TransferEquipment> GetHistory(int id)
        {
            return db.TransferEquipment.Where(t => t.idEquipment == id).OrderByDescending(t=>t.TransferDateTime).ToList();
        }

        [Route("api/Equipments/getcategories")]
        public List<string> GetCategories()
        {
            return db.Model.Select(r => r.Category).Distinct().ToList();
        }

        [Route("api/Equipments/AllModels")]
        public List<ModelCount> GetAllModels()
        {
            return db.Model.
                Select(t => new ModelCount
                {
                    Model = t,
                    Count = t.Equipment.Count()
                }).ToList();
        }

        [Route("api/Equipments/{category}")]
        public List<string> GetTypes(string category)
        {
            return db.Model.Where(t => t.Category == category).Select(r => r.EquipmentType).Distinct().ToList();
        }

        [Route("api/Equipments/search/isnOrSerial/{argument}")]
        public List<Equipment> GetSearchEquipmentByIsnOrSerial(string argument)
        {
            List<Equipment> equipments = db.Equipment.Where(s => s.ISNumber == argument || s.SerialNumber == argument).ToList();
            if (equipments.Count == 0)
            {
                return null;
            }
            return equipments;
        }

        [Route("api/Equipments/{category}/{type}")]
        public List<ModelCount> GetModels(string category, string type)
        {
            return db.Model.
                Where(t => t.Category == category && t.EquipmentType == type).
                Select(t => new ModelCount
                {
                    Model = t,
                    Count = t.Equipment.Count()
                }).ToList();
        }
               

        [Route("api/Equipments/{category}/{type}/{idmodel:int}")]
        public List<Equipment> GetEquipments(string category, string type, int idmodel)
        {
            return db.Equipment.Where(t => t.Model.Category == category && t.Model.EquipmentType == type && t.IDModel == idmodel).ToList();
        }

        [Route("api/Equipments/Model/{idmodel:int}")]
        public Model GetModel(int idModel)
        {
            return db.Model.Where(t => t.IDModel == idModel).FirstOrDefault();
        }

        [Route("api/Equipments/Users")]
        public List<Manager> GetAllUsers()
        {
            return db.Manager.ToList();
        }
                
        // PUT: api/Equipments/5
        [ResponseType(typeof(void))]
        [Route("api/Equipments/{id:int}")]
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

            return Ok(equipment);
        }

        //PUT transfer equipment
        [ResponseType(typeof(string))]
        [Route("api/Equipments/TransferEquipments")]
        public IHttpActionResult PutTransferEquipment(TransferInfo transferInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            foreach (var eq in transferInfo.Equipments)
            {
                if (transferInfo.NewPosition != eq.PositionState)
                {
                    db.TransferEquipment.Add(new TransferEquipment()
                    {
                        idEquipment = eq.IDEquipment,
                        idManager = transferInfo.UserID,
                        TransferDateTime = DateTime.Now,
                        TransferFrom = eq.PositionState,
                        TransferTo = transferInfo.NewPosition
                    });

                    eq.PositionState = transferInfo.NewPosition;
                    db.Entry(eq).State = EntityState.Modified;
                }
            }
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            return Ok(transferInfo.NewPosition);
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
            return Ok(equipment);
           // return CreatedAtRoute("DefaultApi", new { id = equipment.IDEquipment }, equipment);
        }

        // DELETE: api/Equipments/5
        [ResponseType(typeof(Equipment))]
        [Route("api/Equipments/{id:int}")]
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