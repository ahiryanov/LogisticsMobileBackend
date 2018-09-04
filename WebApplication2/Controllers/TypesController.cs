using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication2.Controllers
{
    public class TypesController : ApiController
    {
        LogisticsEntities db = new LogisticsEntities();

        public List<string> GetTypes(string category)
        {
            return db.Model.Where(t => t.Category == category).Select(r => r.EquipmentType).Distinct().ToList();
        }
    }
}
