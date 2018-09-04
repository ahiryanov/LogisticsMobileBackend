using System.Collections.Generic;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class BookController : ApiController
    {
        // GET: api/Book
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Book/5
        public Book Get(int id)
        {
            return new Book { Id = 90, Email = "sdfjkd@sdj.ru", Name = "Мастер и Маргарита", Phone = "89265390975" };
        }

        // POST: api/Book
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Book/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Book/5
        public void Delete(int id)
        {
        }
    }
}
