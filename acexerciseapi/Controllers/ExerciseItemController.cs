using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace acexerciseapi.Controllers
{
    [Produces("application/json")]
    [Route("api/ExerciseItem")]
    public class ExerciseItemController : Controller
    {
        // GET: api/ExerciseItem
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ExerciseItem/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/ExerciseItem
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/ExerciseItem/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
